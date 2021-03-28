using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        Task<IDataResult<List<Rental>>> GetAllAsync();

        Task<IDataResult<int?>> GetCustomerIdByIdAsync(int id);

        Task<IDataResult<List<RentalDto>>> GetAllDtoAsync();

        Task<IDataResult<Rental>> AddAsync(RentalAddDto rentalCreateDto);

        Task<IDataResult<List<Rental>>> GetListReturnDateIsNullAsync();

        Task<IDataResult<Rental>> GetByIdAsync(int id);

        Task<IResult> UpdateAsync(Rental brand);

       Task<IResult> DeleteAsync(Rental brand);
    }
}