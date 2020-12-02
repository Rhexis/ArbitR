using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using ArbitR.Core;
using ArbitR.Core.Event;
using ArbitR.Core.Extensions;
using ArbitR.Dapper.Database;
using ArbitR.Dapper.Handlers;
using ArbitR.Dapper.Pipeline;

namespace ArbitR.Dapper.Services
{
    public abstract class ServiceBase : IService
    {
        // Lazily get Arbiter so that if there isn't anything to raise we aren't waisting time.
        private IArbiter? _arbiter;
        private IArbiter Arbiter => _arbiter ??= ServiceFactory.GetInstance<IArbiter>();
        private readonly Queue<IEvent> _eventQueue = new();
        protected readonly DbContext DbContext;
        protected readonly IDbConnectionFactory DbConnectionFactory = ServiceFactory.GetInstance<IDbConnectionFactory>();
        
        internal ServiceBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        private bool CanHandle(IEvent eEvent)
        {
            Type type = typeof(IMightHandleEvent<>).MakeGenericType(eEvent.GetType());
            if (!GetType().Implements(type)) return true;
            return (bool) Invoke(nameof(CanHandle), new object[] {eEvent})!;
        }
        
        public void Handle(IEvent eEvent)
        {
            var success = false;
            using IDbConnection db = DbConnectionFactory.CreateDbConnection(DbContext);
            using TransactionScope scope = IDbConnectionFactory.CreateTransactionScope();
            try
            {
                if (CanHandle(eEvent))
                {
                    db.Open();
                    Invoke(nameof(IHandleEvent<IEvent>.Handle), new object[] {db, eEvent});
                    scope.Complete();
                }
                success = true;
            }
            finally
            {
                scope.Dispose();
                db.Close();
                db.Dispose();
                RaiseQueuedEvents(success);
            }
        }
        
        protected object? Invoke(string method, object[] args)
        {
            return GetType().InvokeMember
            (
                method,
                BindingFlags.InvokeMethod,
                null,
                this,
                args
            );
        }
        
        /// <summary>
        /// Enqueues and event for raising on successful completion of handling.
        /// Items set to be raised are done so in the order in which they are raised.
        /// </summary>
        /// <param name="eEvent">The event to raise.</param>
        protected void Enqueue(IEvent eEvent)
        {
            _eventQueue.Enqueue(eEvent);
        }
        
        protected void RaiseQueuedEvents(bool success)
        {
            // If we didn't succeed, don't raise any events.
            // This is so that if a command or event that raises more events
            // is replayed then the events won't be raised multiple times.
            if (!success) _eventQueue.Clear();
            if (!_eventQueue.Any()) return;
            try
            {
                foreach (IEvent eEvent in _eventQueue)
                {
                    Arbiter.Raise(eEvent);
                }
            }
            finally
            {
                // Try raise the events, wipe the queue at the end of it all so events aren't raised more than once.
                _eventQueue.Clear();
            }
        }
    }
}