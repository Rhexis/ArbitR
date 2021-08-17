using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArbitR.Core.Extensions
{
    internal static class TypeExtensions
    {
        public static bool Implements(this Type me, Type type)
        {
            if (me == type) return true;
            
            if (type.IsGenericType)
            {
                return me.GetInterfaces().Any
                (i => 
                    i == type 
                    || i.IsGenericType && i.GetGenericTypeDefinition() == type
                );
            }

            return me.GetInterfaces().Any(i => i == type);
        }
        
        public static bool CanBeCastTo(this Type fromType, Type toType)
        {
            return fromType == toType || toType.GetTypeInfo().IsAssignableFrom(fromType.GetTypeInfo());
        }

        public static bool IsOpenGeneric(this Type type)
        {
            return type.GetTypeInfo().IsGenericTypeDefinition || type.GetTypeInfo().ContainsGenericParameters;
        }

        public static IEnumerable<Type> FindInterfaces(this Type fromType, Type templateType)
        {
            if (!fromType.IsConcrete() || !templateType.GetTypeInfo().IsInterface)
            {
                return Enumerable.Empty<Type>();
            }
            
            return fromType.GetInterfaces().Where
            (type =>
                type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == templateType
            ).Distinct();
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
        }
    }
}