using System;
using System.Collections.Generic;
using ArbitR.Internal.Extensions;

namespace ArbitR.Internal
{
    internal delegate object ServiceFactory(Type serviceType);
    internal static class ServiceFactoryExtensions
    {
        public static T GetInstance<T>(this ServiceFactory factory)
            => factory(typeof(T)).Unbox<T>();

        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory)
            => factory(typeof(IEnumerable<T>)).Unbox<IEnumerable<T>>();
        
        public static object? GetInstance(this ServiceFactory factory, Type type) 
            => factory(type).Box();
        
        public static IEnumerable<object> GetInstances(this ServiceFactory factory, Type type)
            => (IEnumerable<object>) factory(typeof(IEnumerable<>).MakeGenericType(type));
    }
}