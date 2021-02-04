namespace Core.Business.Results.Abstract
{
    public interface IBusinessDataResult<TData> : IBusinessResult
    {
        public TData Data { get; }
    }
}
