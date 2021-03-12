using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear =car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id
                             };
                return result.ToList();
            }
        }
    }
}