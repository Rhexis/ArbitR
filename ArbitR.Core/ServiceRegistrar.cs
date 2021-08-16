using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Core
{
    internal static class ServiceRegistrar
    {
        public static void Register(IServiceCollection services, Assembly[] assemblies, IEnumerable<Type> handlers)
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