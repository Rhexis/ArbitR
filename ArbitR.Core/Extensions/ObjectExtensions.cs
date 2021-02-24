namespace ArbitR.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T Unbox<T>(this object? o)
        {
            return (T)o!;
        }

        public static T Box<T>(this object o)
        {
            return (T) o;
        }
    }
}