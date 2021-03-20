using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<Customer> GetById(int id);

        IDataResult<List<Customer>> GetAll();

        IResult Add(CustomerAddDto customerCreateDto);

        IResult Update(Customer customer);

        IResult Update(int id, Customer newCustomer);

        IResult Delete(Customer customer);

        IResult DeleteById(int id);

        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}