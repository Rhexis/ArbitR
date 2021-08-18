namespace ArbitR.Internal.Extensions
{
    internal static class ObjectExtensions
    {
        public static T Unbox<T>(this object? o)
        {
            return (T)o!;
        }

        public static object? Box<T>(this T o)
        {
            return o;
        }
    }
}