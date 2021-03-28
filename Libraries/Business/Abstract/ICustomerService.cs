using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<CustomerDetailDto>> GetCustomerDetailByEmailAsync(string email);

        Task<IDataResult<Customer>> GetByIdAsync(int id);

        Task<IDataResult<List<Customer>>> GetAllAsync();

        Task<IResult> AddAsync(CustomerAddDto customerCreateDto);

        Task<IResult> UpdateAsync(Customer customer);

        Task<IResult> UpdateWithUserAsync(CustomerUpdateDto customerUpdateDto);

        Task<IResult> DeleteAsync(Customer customer);

        Task<IResult> DeleteByIdAsync(int id);

        Task<IDataResult<List<CustomerDetailDto>>> GetCustomerDetailsAsync();
    }
}