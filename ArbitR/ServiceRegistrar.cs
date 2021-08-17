using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Core;
using ArbitR.Core.Extensions;
using ArbitR.Handlers;
using ArbitR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR
{
    public static class ServiceRegistrar
    {
        /// <summary>
        /// Sets up and configures ArbitR.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        /// <param name="assemblies">The Assemblies that contains the read/write services & read model managers.</param>
        public static void AddArbitR(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient<IArbiter, Arbiter>();
            Register
            (
                services,
                assemblies.Distinct().ToArray(),
                new []
                {
                    typeof(IHandleCommand<>),
                    typeof(IHandleEvent<>),
                    typeof(IHandleQuery<,>),
                }
            );
        }
        
        private static void Register(IServiceCollection services, Assembly[] assemblies, IEnumerable<Type> handlers)
        {
            services.AddTransient<ServiceFactory>(p => p.GetService!);
            
            foreach (var handler in handlers)
            {
                services.GenerateTransientServices(handler, assemblies);
            }
        }

        private static void GenerateTransientServices(this IServiceCollection services, Type handlerInterface, IEnumerable<Assembly> assemblies)
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
                    services.AddTransient(type, implementation);
                }
            }
        }
    }
}