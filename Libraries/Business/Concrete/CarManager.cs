using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IRentalService _rentalService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarManager(ICarDal carDal, IRentalService rentalService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            if (car.DailyPrice <= 0)
                return new ErrorResult(Messages.CarDailyPriceInvalid);

            if (car.Description.Length <= 2)
                return new ErrorResult(Messages.CarDescriptionInvalid);

            bool addResult = _carDal.Add(car);

            if (addResult == true)
                return new SuccessResult(Messages.CarAdded);
            else
                return new ErrorResult(Messages.CarAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            bool deleteResult = _carDal.Delete(car);

            if (deleteResult == true)
                return new SuccessResult(Messages.CarDeleted);
            else
                return new ErrorResult(Messages.CarNotAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            var data = _carDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFound);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByRegistered);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            var data = _carDal.Get(p => p.Id == id);

            if (data == null)
                return new ErrorDataResult<Car>(data, Messages.CarNotFound);
            else
                return new SuccessDataResult<Car>(data, Messages.CarGet);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var data = _carDal.GetCarDetails();

            SetCarImage(data);

            if (data.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(data, Messages.CarNotFound);

            return new SuccessDataResult<List<CarDetailDto>>(data, Messages.CarGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            var data = _carDal.GetCarDetailsByBrandId(brandId);

            SetCarImage(data);

            if (data.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByBrand);

            return new SuccessDataResult<List<CarDetailDto>>(data, Messages.CarGetListByBrand);
        }

       

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var data = _carDal.GetAll(p => p.ColorId == colorId);

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFoundByColor);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByColor);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Car>> GetRentalCars()
        {
            var rentalResult = _rentalService.GetListReturnDateIsNull();

            var cars = _carDal.GetAll();

            foreach (var rental in rentalResult.Data)
                cars.Remove(cars.Where(p => p.Id == rental.CarId).First());

            return new SuccessDataResult<List<Car>>(cars);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            bool updateResult = _carDal.Update(car);

            if (updateResult == true)
                return new SuccessResult(Messages.CarUpdated);
            else
                return new ErrorResult(Messages.CarNotUpdated);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(int id, Car newCar)
        {
            var findedEntityResult = GetById(id);

            if (!findedEntityResult.Success)
                return findedEntityResult;

            Car carToUpdate = InputToCar(findedEntityResult.Data, newCar);

            return Update(carToUpdate);
        }


        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            var data = _carDal.GetCarDetailsByColorId(colorId);

            SetCarImage(data);

            if (data.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByColor);

            return new SuccessDataResult<List<CarDetailDto>>(data, Messages.CarGetListByColor);
        }

        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var data = _carDal.GetCarDetailById(id);

            SetCarImage(data);

            if (data == null)
                return new ErrorDataResult<CarDetailDto>(null, Messages.CarNotFoundById);

            return new SuccessDataResult<CarDetailDto>(data, Messages.CarBroughtById);
        }

        private void SetCarImage(List<CarDetailDto> data)
        {
            data.ForEach(carDetail =>
            {
                if (string.IsNullOrEmpty(carDetail.ImagePath))
                    carDetail.ImagePath = DefaultValues.DefaultCarImageUrl;
                else
                {
                    carDetail.ImagePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/" + carDetail.ImagePath;
                }
            });
        }
        private void SetCarImage(List<CarImage> data)
        {
            data.ForEach(carImage =>
            {
                if (string.IsNullOrEmpty(carImage.ImagePath))
                    carImage.ImagePath = DefaultValues.DefaultCarImageUrl;
                else
                {
                    carImage.ImagePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/" + carImage.ImagePath;
                }
            });
        }
        private void SetCarImage(CarImage data)
        {
            if (string.IsNullOrEmpty(data.ImagePath))
                data.ImagePath = DefaultValues.DefaultCarImageUrl;
            else
            {
                data.ImagePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/" + data.ImagePath;
            }
        }
        private void SetCarImage(CarDetailDto data)
        {
            if (string.IsNullOrEmpty(data.ImagePath))
                data.ImagePath = DefaultValues.DefaultCarImageUrl;
            else
            {
                data.ImagePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/" + data.ImagePath;
            }
        }

        private Car InputToCar(Car oldCar, Car newCar)
        {
            oldCar.BrandId = newCar.BrandId;
            oldCar.ColorId = newCar.ColorId;
            oldCar.DailyPrice = newCar.DailyPrice;
            oldCar.ModelYear = newCar.ModelYear;
            oldCar.Description = newCar.Description;
            return oldCar;
        }

    }
}