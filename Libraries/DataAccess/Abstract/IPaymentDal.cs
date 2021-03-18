using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IPaymentDal : IEfRepository<Payment>
    {
    }
}
