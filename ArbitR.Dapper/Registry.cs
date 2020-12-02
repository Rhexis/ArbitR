using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Core;
using ArbitR.Dapper.Database;
using ArbitR.Dapper.Handlers;
using ArbitR.Dapper.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Dapper
{
    public static class Registry
    {
        /// <summary>
        /// Sets up and configures ArbitR.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        /// <param name="assembly">The Assembly that contains the read and write model managers.</param>
        /// <param name="dbContexts">Dictionary mapping read/write connection strings.</param>
        /// <param name="raiseAuditEvent">Raises Audit Event after each request if true.</param>
        public static void AddArbitR
        (
            this IServiceCollection services,
            Assembly assembly,
            Dictionary<DbContext, string> dbContexts,
            bool raiseAuditEvent = false
        )
        {
            services.AddArbitR(new[] {assembly}, dbContexts, raiseAuditEvent);
        }
        
        /// <summary>
        /// Sets up and configures ArbitR.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        /// <param name="dbContexts">Dictionary mapping read/write connection strings.</param>
        /// <param name="assemblies">The Assemblies that contains the read and write model managers.</param>
        /// /// <param name="raiseAuditEvent">Raises Audit Event after each request if true.</param>
        public static void AddArbitR
        (
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies,
            Dictionary<DbContext, string> dbContexts,
            bool raiseAuditEvent = false
        )
        {
            services.AddScoped<IArbiter, Arbiter>();
            services.AddSingleton<IDictionary<DbContext, string>>(dbContexts);
            services.AddSingleton<IDbConnectionFactory, DefaultDbConnectionFactory>();
            Config.Configure(raiseAuditEvent);
            ServiceFactory.Initialize
            (
                services,
                assemblies.Distinct().ToArray(),
                new []
                {
                    typeof(IHandleCommand<>),
                    typeof(IHandleEvent<>),
                    typeof(IHandleQuery<,>)
                }
            );
        }
    }
}