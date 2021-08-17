﻿using System.Linq;
using System.Reflection;
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
            Core.ServiceRegistrar.Register
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
    }
}