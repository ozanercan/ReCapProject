namespace Core.Business.Results.Abstract
{
    public interface IBusinessResult
    {
        public string Message { get; }
        public bool IsSuccess { get; }
    }
}
