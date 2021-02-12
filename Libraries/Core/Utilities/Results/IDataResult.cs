namespace Core.Utilities.Results
{
    public interface IDataResult<TData> : IResult
    {
        public TData Data { get; }
    }
}
