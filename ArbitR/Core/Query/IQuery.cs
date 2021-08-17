namespace ArbitR.Core.Query
{
    public interface IQuery
    {
    }
    public interface IQuery<out T> : IQuery
    {
    }
}