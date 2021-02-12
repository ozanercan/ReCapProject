namespace Core.Utilities.Results
{
    public class ErrorDataResult<TData> : DataResult<TData>
    {
        public ErrorDataResult(TData data) : base(false, data)
        {
        }

        public ErrorDataResult(TData data, string message) : base(false, message, data)
        {
        }
    }
}
