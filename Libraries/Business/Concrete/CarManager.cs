using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IRentalService _rentalService;
        private readonly ICarImageService _carImageService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;
        private readonly ICarCreditScoreService _carCreditScoreService;
        private readonly IFuelTypeService _fuelTypeService;
        private readonly IGearTypeService _gearTypeService;

        public CarManager(ICarDal carDal, IRentalService rentalService, ICarImageService carImageService, IBrandService brandService, IColorService colorService, ICarCreditScoreService carCreditScoreService, IFuelTypeService fuelTypeService, IGearTypeService gearTypeService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
            _carImageService = carImageService;
            _brandService = brandService;
            _colorService = colorService;
            _carCreditScoreService = carCreditScoreService;
            _fuelTypeService = fuelTypeService;
            _gearTypeService = gearTypeService;
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarAddDtoValidator))]
        public async Task<IResult> AddAsync(CarAddDto carAddDto)
        {
            var brandResult = await GetBrandByBrandNameAsync(carAddDto.BrandName);
            if (!brandResult.Success)
                return new ErrorResult(brandResult.Message);

            var colorResult = await GetColorByColorNameAsync(carAddDto.ColorName);
            if (!colorResult.Success)
                return new ErrorResult(colorResult.Message);

            var gearTypeResult = await GetGearTypeByGearTypeNameAsync(carAddDto.GearTypeName);
            if (!gearTypeResult.Success)
                return new ErrorResult(gearTypeResult.Message);

            var fuelTypeResult = await GetFuelTypeByFuelTypeNameAsync(carAddDto.FuelTypeName);
            if (!fuelTypeResult.Success)
                return new ErrorResult(fuelTypeResult.Message);

            Car carToAdd = new Car()
            {
                BrandId = brandResult.Data.Id,
                ColorId = colorResult.Data.Id,
                FuelTypeId = fuelTypeResult.Data.Id,
                GearTypeId = gearTypeResult.Data.Id,
                HorsePower = carAddDto.HorsePower,
                Name = carAddDto.Name,
                DailyPrice = carAddDto.DailyPrice,
                Description = carAddDto.Description,
                ModelYear = carAddDto.ModelYear
            };
            bool addResult = await _carDal.AddAsync(carToAdd);

            if (!addResult)
                return new ErrorResult(Messages.CarNotAdded);

            var carCreditScoreAddResult = await AddCarCreditScoreAsync(carToAdd.Id, carAddDto.MinCreditScore);
            if (!carCreditScoreAddResult.Success)
                return new ErrorResult(carCreditScoreAddResult.Message);

            return new SuccessResult(Messages.CarAdded);
        }

        private async Task<IResult> AddCarCreditScoreAsync(int carId, int minCreditScore)
        {
            var carCreditScoreResult = await _carCreditScoreService.AddAsync(new CarCreditScoreAddDto()
            {
                CarId = carId,
                MinCreditScore = minCreditScore
            });

            return carCreditScoreResult;
        }

        [CacheRemoveAspect("ICarService.Get")]
        public async Task<IResult> DeleteAsync(Car car)
        {
            bool deleteResult = await _carDal.DeleteAsync(car);

            if (!deleteResult)
                return new ErrorResult(Messages.CarNotAdded);

            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var getResult = await this.GetByIdAsync(id);

            if (!getResult.Success)
                return getResult;

            return await DeleteAsync(getResult.Data);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Car>>> GetAllAsync()
        {
            var data = await _carDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<Car>>(null, Messages.CarNotFound);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByRegistered);
        }

        [PerformanceAspect(5)]
        public async Task<IDataResult<Car>> GetByIdAsync(int id)
        {
            var data = await _carDal.GetAsync(p => p.Id == id);

            if (data == null)
                return new ErrorDataResult<Car>(null, Messages.CarNotFound);
            else
                return new SuccessDataResult<Car>(data, Messages.CarGet);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsAsync()
        {
            var carDetails = await _carDal.GetCarDetailsAsync();
            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFound);

            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByRegistered);
        }

        //private void SetImages(List<CarDetailDto> carDetails, List<CarImage> carImages)
        //{
        //    carDetails.ForEach(carDetail =>
        //    {
        //        var findedCarImages = carImages.Where(p => p.CarId == carDetail.Id).ToList();
        //        if (findedCarImages.Count == 0)
        //        {
        //            carDetail.ImagePaths.Add(_carImageService.GetDefaultCarImage(carDetail.Id).Data);
        //        }
        //        else
        //        {
        //            carDetail.ImagePaths = findedCarImages;
        //        }
        //    });
        //}

        //private void SetImages(CarDetailDto carDetail, List<CarImage> carImages)
        //{
        //    var findedCarImages = carImages.Where(p => p.CarId == carDetail.Id).ToList();
        //    if (findedCarImages.Count == 0)
        //    {
        //        carDetail.ImagePaths.Add(_carImageService.GetDefaultCarImage(carDetail.Id).Data);
        //    }
        //    else
        //    {
        //        carDetail.ImagePaths = findedCarImages;
        //    }
        //}

        //private void CheckDefaultImage(List<CarDetailDto> data)
        //{
        //    foreach (var item in data)
        //    {
        //        if (item.ImagePaths.Count == 0)
        //        {
        //            item.ImagePaths = new List<CarImage>() { _carImageService.GetDefaultCarImage(item.Id).Data };
        //        }
        //    }
        //}
        //private void CheckDefaultImage(CarDetailDto data)
        //{
        //    if (data.ImagePaths.Count == 0)
        //    {
        //        data.ImagePaths = new List<CarImage>() { _carImageService.GetDefaultCarImage(data.Id).Data };
        //    }
        //}

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByBrandIdAsync(int brandId)
        {
            var carDetails = await _carDal.GetCarDetailsByBrandIdAsync(brandId);

            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByBrand);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByBrand);
        }



        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var data = _carDal.GetAllNoTracking(p => p.ColorId == colorId);

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFoundByColor);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByColor);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Car>>> GetRentalCarsAsync()
        {
            var rentalResult = await _rentalService.GetListReturnDateIsNullAsync();

            var cars = await _carDal.GetAllAsync();

            foreach (var rental in rentalResult.Data)
                cars.Remove(cars.Where(p => p.Id == rental.CarId).First());

            return new SuccessDataResult<List<Car>>(cars);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarUpdateDtoValidator))]
        public async Task<IResult> UpdateAsync(CarUpdateDto carUpdateDto)
        {
            var findedEntity = _carDal.Get(p => p.Id == carUpdateDto.Id);
            if (findedEntity == null)
                return new ErrorResult(Messages.CarNotFound);

            var colorResult = await _colorService.GetByNameAsync(carUpdateDto.ColorName);
            if (!colorResult.Success)
                return colorResult;

            var brandResult = await _brandService.GetByNameAsync(carUpdateDto.BrandName);
            if (!brandResult.Success)
                return brandResult;

            var gearTypeResult = await GetGearTypeByGearTypeNameAsync(carUpdateDto.GearTypeName);
            if (!gearTypeResult.Success)
                return new ErrorResult(gearTypeResult.Message);

            var fuelTypeResult = await GetFuelTypeByFuelTypeNameAsync(carUpdateDto.FuelTypeName);
            if (!fuelTypeResult.Success)
                return new ErrorResult(fuelTypeResult.Message);

            var carCreditScoreResult = await _carCreditScoreService.UpdateAsync(new CarCreditScoreUpdateDto()
            {
                MinCreditScore = carUpdateDto.MinCreditScore,
                CarId = carUpdateDto.Id
            });
            if (!carCreditScoreResult.Success)
                return carCreditScoreResult;

            findedEntity.BrandId = brandResult.Data.Id;
            findedEntity.ColorId = colorResult.Data.Id;
            findedEntity.ModelYear = carUpdateDto.ModelYear;
            findedEntity.DailyPrice = carUpdateDto.DailyPrice;
            findedEntity.Description = carUpdateDto.Description;
            findedEntity.HorsePower = carUpdateDto.HorsePower;
            findedEntity.Name = carUpdateDto.Name;
            findedEntity.GearTypeId = gearTypeResult.Data.Id;
            findedEntity.FuelTypeId = fuelTypeResult.Data.Id;

            bool updateResult = await _carDal.UpdateAsync(findedEntity);

            if (updateResult == true)
                return new SuccessResult(Messages.CarUpdated);
            else
                return new ErrorResult(Messages.CarNotUpdated);
        }

        [CacheAspect]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByColorIdAsync(int colorId)
        {
            var carDetails = await _carDal.GetCarDetailsByColorIdAsync(colorId);

            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }


            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByColor);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByColor);
        }

        [CacheAspect]
        public async Task<IDataResult<CarDetailDto>> GetCarDetailByIdAsync(int id)
        {
            var carDetail = await _carDal.GetCarDetailByIdAsync(id);

            carDetail.ImagePaths = (await _carImageService.GetAllByCarIdAsync(id)).Data;

            if (carDetail == null)
                return new ErrorDataResult<CarDetailDto>(null, Messages.CarNotFoundById);

            return new SuccessDataResult<CarDetailDto>(carDetail, Messages.CarBroughtById);
        }

        [CacheAspect]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByBrandNameAsync(string brandName)
        {
            var carDetails = await _carDal.GetCarDetailsByBrandNameAsync(brandName);

            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByBrand);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByBrand);
        }

        [CacheAspect]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByColorNameAsync(string colorName)
        {
            var carDetails = await _carDal.GetCarDetailsByColorNameAsync(colorName);


            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }


            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByColor);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByColor);
        }

        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetailsByFiltersAsync(CarFilterDto carFilterDto)
        {
            var carDetails = await _carDal.GetCarDetailsByFilterAsync(carFilterDto);

            foreach (var item in carDetails)
            {
                item.ImagePaths = (await _carImageService.GetAllByCarIdAsync(item.Id)).Data;
            }

            if (carDetails.Count == 0)
                return new ErrorDataResult<List<CarDetailDto>>(null, Messages.CarNotFoundByFilters);

            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.CarGetListByFilters);
        }

        [CacheAspect]
        public async Task<IDataResult<decimal>> GetMoneyToPaidByRentalIdAsync(int rentalId)
        {
            var rentalResult = await _rentalService.GetByIdAsync(rentalId);
            if (!rentalResult.Success)
                return new ErrorDataResult<decimal>(0, rentalResult.Message);

            int carId = rentalResult.Data.CarId;

            var carResult = await this.GetByIdAsync(carId);
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

        private async Task<IDataResult<Brand>> GetBrandByBrandNameAsync(string brandName)
        {
            return await _brandService.GetByNameAsync(brandName);
        }
        private async Task<IDataResult<Color>> GetColorByColorNameAsync(string colorName)
        {
            return await _colorService.GetByNameAsync(colorName);
        }
        private async Task<IDataResult<FuelType>> GetFuelTypeByFuelTypeNameAsync(string fuelTypeName)
        {
            return await _fuelTypeService.GetByNameAsync(fuelTypeName);
        }
        private async Task<IDataResult<GearType>> GetGearTypeByGearTypeNameAsync(string gearTypeName)
        {
            return await _gearTypeService.GetByNameAsync(gearTypeName);
        }

        public async Task<IDataResult<CarUpdateDto>> GetUpdateDtoByIdAsync(int carId)
        {
            var carResult = await this.GetCarDetailByIdAsync(carId);
            if (!carResult.Success)
                return new ErrorDataResult<CarUpdateDto>(null, carResult.Message);

            CarUpdateDto carUpdateDto = new CarUpdateDto()
            {
                Id = carResult.Data.Id,
                BrandName = carResult.Data.BrandName,
                ColorName = carResult.Data.ColorName,
                DailyPrice = carResult.Data.DailyPrice,
                Description = carResult.Data.Description,
                MinCreditScore = carResult.Data.MinCreditScore,
                ModelYear = carResult.Data.ModelYear,
                FuelTypeName = carResult.Data.FuelTypeName,
                GearTypeName = carResult.Data.GearTypeName,
                HorsePower = carResult.Data.HorsePower,
                Name = carResult.Data.Name
            };

            return new SuccessDataResult<CarUpdateDto>(carUpdateDto, Messages.CarUpdateDtoBrought);
        }
    }
}