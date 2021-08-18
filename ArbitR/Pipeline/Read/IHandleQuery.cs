namespace ArbitR.Pipeline.Read
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}