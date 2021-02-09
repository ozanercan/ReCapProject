using Core.Business.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IBusinessDataResult<List<CarDetailDto>> GetCarDetails();

        IBusinessDataResult<List<Car>> GetCarsByBrandId(int brandId);

        IBusinessDataResult<List<Car>> GetCarsByColorId(int colorId);

        IBusinessDataResult<Car> GetById(int id);

        IBusinessDataResult<List<Car>> GetAll();

        IBusinessResult Add(Car car);

        IBusinessResult Update(Car car);

        IBusinessResult Update(int id, Car newCar);

        IBusinessResult Delete(Car car);

        IBusinessResult DeleteById(int id);
    }
}