using Core.Business.Results.Abstract;

namespace Core.Business.Results.Concrete
{
    public class BusinessResult : IBusinessResult
    {
        public BusinessResult(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Message { get; }
        public bool IsSuccess { get; }
    }
}