using System.Linq;
using System.Reflection;
using ArbitR.Core;
using ArbitR.Handlers;
using ArbitR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR
{
    public static class Registry
    {
        /// <summary>
        /// Sets up and configures ArbitR.
        /// </summary>
        /// <param name="services">Service collection container.</param>
        /// <param name="assemblies">The Assemblies that contains the read and write model managers.</param>
        public static void AddArbitR(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient<IArbiter, Arbiter>();
            ServiceFactory.Initialize
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