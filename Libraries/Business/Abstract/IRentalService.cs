using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();

        IDataResult<int?> GetCustomerIdById(int id);

        IDataResult<List<RentalDto>> GetAllDto();

        IDataResult<Rental> Add(RentalAddDto rentalCreateDto);

        IDataResult<List<Rental>> GetListReturnDateIsNull();

        IDataResult<Rental> GetById(int id);

        IResult Update(Rental brand);

        IResult Delete(Rental brand);
    }
}