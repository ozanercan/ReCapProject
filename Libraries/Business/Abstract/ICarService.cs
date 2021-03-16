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

        IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId);

        IDataResult<CarDetailDto> GetCarDetailById(int id);

        IDataResult<Car> GetById(int id);

        IDataResult<List<Car>> GetAll();

        IResult Add(Car car);

        IResult Update(Car car);

        IResult Update(int id, Car newCar);

        IResult Delete(Car car);

        IResult DeleteById(int id);
    }
}