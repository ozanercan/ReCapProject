using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public async Task<List<CarDetailDto>> GetCarDetailsAsync()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsByBrandIdAsync(int brandId)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where brand.Id == brandId
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsByColorIdAsync(int colorId)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where color.Id == colorId
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }

        public async Task<CarDetailDto> GetCarDetailByIdAsync(int id)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where car.Id == id
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsByBrandNameAsync(string brandName)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where brand.Name == brandName
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsByColorNameAsync(string colorName)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where color.Name == colorName
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsByFilterAsync(CarFilterDto carFilterDto)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             join carCreditScore in context.CarCreditScores
                             on car.Id equals carCreditScore.CarId into gj
                             from x in gj.DefaultIfEmpty()
                             where color.Name == carFilterDto.ColorName
                             && brand.Name == carFilterDto.BrandName
                             select new CarDetailDto
                             {
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Id = car.Id,
                                 MinCreditScore = x.MinCreditScore
                             };
                return await result.AsNoTracking().ToListAsync();
            }
        }
    }
}