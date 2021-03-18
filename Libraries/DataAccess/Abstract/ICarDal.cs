using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEfRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        List<CarDetailDto> GetCarDetailsByBrandId(int brandId);
        List<CarDetailDto> GetCarDetailsByColorId(int colorId);
        CarDetailDto GetCarDetailById(int id);
        List<CarDetailDto> GetCarDetailsByBrandName(string brandName);
        List<CarDetailDto> GetCarDetailsByColorName(string colorName);
        List<CarDetailDto> GetCarDetailsByFilter(CarFilterDto carFilterDto);
    }
}