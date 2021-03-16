using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
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
                             join carImage in context.CarImages
                             on car.Id equals carImage.CarId into cimg
                             from x in cimg.DefaultIfEmpty()
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 ImagePath = x.ImagePath
                             };
                return result.AsNoTracking().ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carImage in context.CarImages
                             on car.Id equals carImage.CarId into cimg
                             from x in cimg.DefaultIfEmpty()
                             where brand.Id == brandId
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 ImagePath = x.ImagePath
                             };
                return result.AsNoTracking().ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carImage in context.CarImages
                             on car.Id equals carImage.CarId into cimg
                             from x in cimg.DefaultIfEmpty()
                             where color.Id == colorId
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 ImagePath = x.ImagePath
                             };
                return result.AsNoTracking().ToList();
            }
        }

        public CarDetailDto GetCarDetailById(int id)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carImage in context.CarImages
                             on car.Id equals carImage.CarId into cimg
                             from x in cimg.DefaultIfEmpty()
                             where car.Id == id
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 ImagePath = x.ImagePath
                             };
                return result.AsNoTracking().FirstOrDefault();
            }
        }
    }
}