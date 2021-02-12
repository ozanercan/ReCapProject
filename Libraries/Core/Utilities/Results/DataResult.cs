namespace Core.Utilities.Results
{
    public class DataResult<TData> : Result, IDataResult<TData>
    {
        public DataResult(bool success, TData data) : base(success)
        {
            Data = data;
        }

        public DataResult(bool success, string message, TData data) : base(success, message)
        {
            Data = data;
        }

        
        public TData Data { get; }
    }
}
