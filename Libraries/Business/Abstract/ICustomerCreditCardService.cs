using Core.Utilities.Results;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerCreditCardService
    {
        Task<IResult> AddAsync(CustomerCreditCardAddDto customerCreditCartAddDto);
        Task<IDataResult<List<CustomerCreditCardDto>>> GetCardsByCustomerIdAsync(int customerId);
    }
}