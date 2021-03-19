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
using System;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IRentalService _rentalService;
        private readonly ICarImageService _carImageService;

        public CarManager(ICarDal carDal, IRentalService rentalService, ICarImageService carImageService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
            _carImageService = carImageService;
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
            var carDetails = _carDal.GetCarDetails();

            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);

            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(carDetails, Messages.CarNotFound);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByRegistered);
        }

        private void SetImages(List<CarDetailDto> carDetails, List<CarImage> carImages)
        {
            carDetails.ForEach(carDetail =>
            {
                var findedCarImages = carImages.Where(p => p.CarId == carDetail.Id).ToList();
                if (findedCarImages.Count == 0)
                {
                    carDetail.ImagePaths.Add(_carImageService.GetDefaultCarImage(carDetail.Id).Data);
                }
                else
                {
                    carDetail.ImagePaths = findedCarImages;
                }
            });
        }

        private void SetImages(CarDetailDto carDetail, List<CarImage> carImages)
        {
            var findedCarImages = carImages.Where(p => p.CarId == carDetail.Id).ToList();
            if (findedCarImages.Count == 0)
            {
                carDetail.ImagePaths.Add(_carImageService.GetDefaultCarImage(carDetail.Id).Data);
            }
            else
            {
                carDetail.ImagePaths = findedCarImages;
            }
        }

        private void CheckDefaultImage(List<CarDetailDto> data)
        {
            foreach (var item in data)
            {
                if (item.ImagePaths.Count == 0)
                {
                    item.ImagePaths = new List<CarImage>() { _carImageService.GetDefaultCarImage(item.Id).Data };
                }
            }
        }
        private void CheckDefaultImage(CarDetailDto data)
        {
            if (data.ImagePaths.Count == 0)
            {
                data.ImagePaths = new List<CarImage>() { _carImageService.GetDefaultCarImage(data.Id).Data };
            }
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            var carDetails = _carDal.GetCarDetailsByBrandId(brandId);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);
            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByBrand);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByBrand);
        }



        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var data = _carDal.GetAllNoTracking(p => p.ColorId == colorId);

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
            var carDetails = _carDal.GetCarDetailsByColorId(colorId);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);
            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByColor);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByColor);
        }

        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var carDetail = _carDal.GetCarDetailById(id);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetail, carImages);
            CheckDefaultImage(carDetail);

            if (carDetail == null)
                return new ErrorDataResult<CarDetailDto>(null, Messages.CarNotFoundById);

            return new SuccessDataResult<CarDetailDto>(carDetail, Messages.CarBroughtById);
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

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandName(string brandName)
        {
            var carDetails = _carDal.GetCarDetailsByBrandName(brandName);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);
            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByBrand);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByBrand);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorName(string colorName)
        {
            var carDetails = _carDal.GetCarDetailsByColorName(colorName);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);
            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByColor);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByColor);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByFilters(CarFilterDto carFilterDto)
        {
            var carDetails = _carDal.GetCarDetailsByFilter(carFilterDto);
            var carImages = _carImageService.GetAllNoTracking().Data;

            SetImages(carDetails, carImages);
            CheckDefaultImage(carDetails);

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByFilters);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByFilters);
        }

        public IDataResult<decimal> GetMoneyToPaidByRentalId(int rentalId)
        {
            var rentalResult = _rentalService.GetById(rentalId);
            if (!rentalResult.Success)
                return new ErrorDataResult<decimal>(0, rentalResult.Message);

            int carId = rentalResult.Data.CarId;

            var carResult = this.GetById(carId);
            if (!carResult.Success)
                return new ErrorDataResult<decimal>(0, carResult.Message);

            decimal price = CalculateRent(carResult.Data.DailyPrice, rentalResult.Data.RentDate, rentalResult.Data.ReturnDate.Value);

            price = Math.Ceiling(price);
            return new SuccessDataResult<decimal>(price, Messages.CarRentPriceCalculated);
        }

        private decimal CalculateRent(decimal dailyPrice, DateTime rentDate, DateTime returnDate)
        {
            TimeSpan timeSpan = returnDate - rentDate;

            decimal perMinutePrice = (dailyPrice / 24) / 60;

            return Convert.ToDecimal(timeSpan.TotalMinutes) * perMinutePrice;
        }
    }
}