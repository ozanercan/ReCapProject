using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        Task<IDataResult<List<Car>>> GetRentalCarsAsync();

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsAsync();

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByBrandIdAsync(int brandId);

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByBrandNameAsync(string brandName);

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByColorIdAsync(int colorId);

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByColorNameAsync(string colorName);

        Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByFiltersAsync(CarFilterDto carFilterDto);

        Task<IDataResult<CarDetailDto>> GetCarDetailByIdAsync(int id);

        Task<IDataResult<decimal>> GetMoneyToPaidByRentalIdAsync(int rentalId);

        Task<IDataResult<Car>> GetByIdAsync(int id);

        Task<IDataResult<List<Car>>> GetAllAsync();

        Task<IResult> AddAsync(CarAddDto carAddDto);

        Task<IResult> UpdateAsync(CarUpdateDto carUpdateDto);

        Task<IDataResult<CarUpdateDto>> GetUpdateDtoByIdAsync(int carId);

        Task<IResult> DeleteAsync(Car car);

        Task<IResult> DeleteByIdAsync(int id);
    }
}