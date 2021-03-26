using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICustomerCreditCardDal : IEfRepository<CustomerCreditCard>
    {
    }
}