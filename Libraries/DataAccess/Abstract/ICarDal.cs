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
    }
}