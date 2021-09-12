using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Internal;
using ArbitR.Internal.Extensions;
using ArbitR.Internal.Pipeline;
using ArbitR.Internal.Pipeline.Background;
using ArbitR.Pipeline;
using ArbitR.Pipeline.Read;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;
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
            services.AddArbitR(new Config(), assemblies);
        }

        /// <summary>
        /// Sets up and configures ArbitR.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        /// <param name="cfg"></param>
        /// <param name="assemblies">The Assemblies that contains the read/write services & read model managers.</param>
        public static void AddArbitR(this IServiceCollection services, Config cfg, params Assembly[] assemblies)
        {
            Register
            (
                services,
                assemblies.Distinct().ToArray(),
                new []
                {
                    typeof(IHandleCommand<>),
                    typeof(IHandleEvent<>),
                    typeof(IHandleQuery<,>)
                },
                cfg
            );
        }

        private static void Register
        (
            IServiceCollection services,
            Assembly[] assemblies,
            IEnumerable<Type> handlers,
            Config cfg
        )
        {
            services.AddTransient<ServiceFactory>(p => p.GetService!);
            services.AddSingleton<EventService>();
            services.AddHostedService<HostedQueueService>();
            services.AddSingleton<IBackgroundEventQueue>(_ => new DefaultBackgroundEventQueue(cfg.QueueConfiguration));
            services.AddTransient<IArbiter, Arbiter>();
            
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