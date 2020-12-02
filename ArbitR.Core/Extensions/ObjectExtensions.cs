namespace ArbitR.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T Unbox<T>(this object? type)
        {
            return (T)type!;
        }
    }
}