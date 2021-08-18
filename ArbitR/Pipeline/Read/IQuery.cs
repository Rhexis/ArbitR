namespace ArbitR.Pipeline.Read
{
    public interface IQuery
    {
    }
    public interface IQuery<out T> : IQuery
    {
    }
}