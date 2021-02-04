using Core.Business.Results.Abstract;

namespace Core.Business.Results.Concrete
{
    public class BusinessDataResult<TData> : BusinessResult, IBusinessDataResult<TData>
    {
        public BusinessDataResult(string message, bool isSuccess, TData data) : base(message, isSuccess)
        {
            Data = data;
        }
        public TData Data { get; }
    }
}
