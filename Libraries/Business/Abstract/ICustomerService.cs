using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<CustomerDetailDto> GetCustomerDetailByEmail(string email);

        IDataResult<Customer> GetById(int id);

        IDataResult<List<Customer>> GetAll();

        IResult Add(CustomerAddDto customerCreateDto);

        IResult Update(Customer customer);

        IResult UpdateWithUser(CustomerUpdateDto customerUpdateDto);

        IResult Delete(Customer customer);

        IResult DeleteById(int id);

        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}