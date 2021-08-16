using System;
using System.Collections.Generic;

namespace ArbitR.Core
{
    internal delegate object ServiceFactory(Type serviceType);
    internal static class ServiceFactoryExtensions
    {
        public static T GetInstance<T>(this ServiceFactory factory)
            => (T) factory(typeof(T));

        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory)
            => (IEnumerable<T>) factory(typeof(IEnumerable<T>));
        
        public static object? GetInstance(this ServiceFactory factory, Type type) 
            => (object?) factory(type);
        
        public static IEnumerable<object> GetInstances(this ServiceFactory factory, Type type)
            => (IEnumerable<object>) factory(typeof(IEnumerable<>).MakeGenericType(type));
    }
}