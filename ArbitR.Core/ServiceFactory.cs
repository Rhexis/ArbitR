using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ArbitR.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ArbitR")]
[assembly: InternalsVisibleTo("ArbitR.Dapper")]
namespace ArbitR.Core
{
    internal static class ServiceFactory
    {
        private static IServiceCollection? _services;
        private static IServiceProvider? _provider;
        private static IServiceProvider Provider => _provider ??= _services?.BuildServiceProvider()
            ?? throw new InvalidOperationException($"{nameof(ServiceFactory)} has not been initialized");
        
        public static T GetInstance<T>()
        {
            return Provider.GetService<T>();
        }
        
        public static object? GetInstance(Type type)
        {
            return Provider.GetService(type);
        }
        
        public static IEnumerable<T> GetInstances<T>()
        {
            return Provider.GetServices<T>();
        }
        
        public static IEnumerable<object> GetInstances(Type type)
        {
            return Provider.GetServices(type);
        }
        
        public static void Initialize(IServiceCollection services, Assembly[] assemblies, IEnumerable<Type> handlers)
        {
            _services = services;
            
            foreach (var handler in handlers)
            {
                GenerateTransientServices(handler, assemblies);
            }
        }

        private static void GenerateTransientServices(Type handlerInterface, IEnumerable<Assembly> assemblies)
        {
            var concreteTypes = new List<Type>();
            var interfaces = new List<Type>();
            
            foreach (TypeInfo typeInfo in assemblies.SelectMany(a => a.DefinedTypes).Where(t => !t.IsOpenGeneric()))
            {
                Type[] array = typeInfo.FindInterfaces(handlerInterface).ToArray();
                if (!array.Any()) continue;
                
                if (typeInfo.IsConcrete())
                {
                    concreteTypes.Add(typeInfo);
                }

                foreach (Type type in array)
                {
                    if (interfaces.Contains(type)) continue;
                    interfaces.Add(type);
                }
            }

            foreach (Type type in interfaces)
            {
                foreach (Type implementation in concreteTypes.Where(x => x.CanBeCastTo(type)))
                {
                    _services.AddTransient(type, implementation);
                }
            }
        }
    }
}