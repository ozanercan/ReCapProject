using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetRentalCars();

        IDataResult<List<CarDetailDto>> GetCarDetails();

        IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId);

        IDataResult<List<CarDetailDto>> GetCarDetailsByBrandName(string brandName);

        IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId);

        IDataResult<List<CarDetailDto>> GetCarDetailsByColorName(string colorName);

        IDataResult<List<CarDetailDto>> GetCarDetailsByFilters(CarFilterDto carFilterDto);

        IDataResult<CarDetailDto> GetCarDetailById(int id);

        IDataResult<decimal> GetMoneyToPaidByRentalId(int rentalId);

        IDataResult<Car> GetById(int id);

        IDataResult<List<Car>> GetAll();

        IResult Add(CarAddDto carAddDto);

        IResult Update(CarUpdateDto carUpdateDto);

        IResult Delete(Car car);

        IResult DeleteById(int id);
    }
}