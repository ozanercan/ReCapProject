using Core.Utilities.Results;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerCreditCardService
    {
        IResult Add(CustomerCreditCardAddDto customerCreditCartAddDto);
        IDataResult<List<CustomerCreditCardDto>> GetCardsByCustomerId(int customerId);
    }
}