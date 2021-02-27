using Business.Abstract;
using Business.Constants;
using Business.Utilities.FileHelper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly ICarService _carService;
        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(CarImageAddDtoValidator))]
        public async Task<IResult> AddAsync(CarImageAddDto carImageAddDto, IHostEnvironment hostEnvironment)
        {
            var logicResult = BusinessRules.Run(
                CheckIfCarExist(carImageAddDto.CarId),
                CheckIfNumberOfCarPicturesByCarId(carImageAddDto.CarId));

            if (!logicResult.Success)
                return logicResult;

            FileHelper.Initialize(hostEnvironment);

            var uploadResult = await FileHelper.ImageUploadAsync(carImageAddDto.FormFile);
            if (!uploadResult.Success)
                return new ErrorResult(Messages.CarImageNotUploaded);

            CarImage carImage = new CarImage()
            {
                CarId = carImageAddDto.CarId,
                ImagePath = uploadResult.ShortPath,
                Date = DateTime.Now
            };

            var addResult = _carImageDal.Add(carImage);

            if (!addResult)
                return new ErrorResult(Messages.CarImageNotAdded);


            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImageDeleteDto carImageDeleteDto, IHostEnvironment hostEnvironment)
        {
            FileHelper.Initialize(hostEnvironment);

            var carImageResult = this.GetById(carImageDeleteDto.Id);
            if (!carImageResult.Success)
                return new ErrorResult(carImageResult.Message);

            var fileRemoveResult = FileHelper.FileRemove(carImageResult.Data.ImagePath);

            if (!fileRemoveResult.Success)
                return new ErrorResult(Messages.RegisteredCarImageNotDeleted);

            bool deleteResult = _carImageDal.Delete(carImageResult.Data);

            if (!deleteResult)
                return new ErrorResult(Messages.CarImageNotDeleted);

            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll(HttpRequest httpRequest)
        {
            var carImages = _carImageDal.GetAll();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            GetImagePathScheme(httpRequest, carImages);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId, HttpRequest httpRequest)
        {
            var logicResult = BusinessRules.Run(
                CheckIfCarExist(carId));

            if (!logicResult.Success)
                return new ErrorDataResult<List<CarImage>>(null, logicResult.Message);

            var getCarList = _carImageDal.GetAll(p => p.CarId == carId);

            if (getCarList.Count == 0)
            {
                var defaultCarImage = GetDefaultCarImage(carId);
                return new SuccessDataResult<List<CarImage>>(new List<CarImage> { defaultCarImage });
            }

            GetImagePathScheme(httpRequest, getCarList);

            return new SuccessDataResult<List<CarImage>>(getCarList);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var carImage = _carImageDal.Get(p => p.Id == id);

            if (carImage == null)
                return new ErrorDataResult<CarImage>(null, Messages.CarImageNotFound);

            return new SuccessDataResult<CarImage>(carImage, Messages.CarImageBrought);
        }

        public async Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto, IHostEnvironment hostEnvironment)
        {
            FileHelper.Initialize(hostEnvironment);

            var logicResult = BusinessRules.Run(
                CheckIfCarExist(carImageUpdateDto.CarId));

            if (!logicResult.Success)
                return logicResult;

            var carImageResult = this.GetById(carImageUpdateDto.Id);
            if (!carImageResult.Success)
                return new ErrorResult(carImageResult.Message);

            var fileRemoveResult = FileHelper.FileRemove(carImageResult.Data.ImagePath);

            if (!fileRemoveResult.Success)
                return new ErrorResult(Messages.RegisteredCarImageNotDeleted);

            var fileAddResult = await FileHelper.ImageUploadAsync(carImageUpdateDto.FormFile);

            if (!fileAddResult.Success)
                return new ErrorResult(Messages.CarImageNotAdded);

            carImageResult.Data.CarId = carImageUpdateDto.CarId;
            carImageResult.Data.ImagePath = fileAddResult.ShortPath;

            var updateResult = _carImageDal.Update(carImageResult.Data);

            if (!updateResult)
                return new ErrorResult(Messages.CarImageNotUpdated);

            return new SuccessResult(Messages.CarImageUpdated);
        }

        private CarImage GetDefaultCarImage(int carId)
        {
            return new CarImage()
            {
                CarId = carId,
                ImagePath = @"https://cdn1.iconfinder.com/data/icons/cars-journey/91/Cars__Journey_68-512.png"
            };
        }

        private void GetImagePathScheme(HttpRequest httpRequest, List<CarImage> getCarList)
        {
            getCarList.ForEach(p =>
            {
                p.ImagePath = string.Join(@"/", httpRequest.Scheme + ":/", httpRequest.Host.Value, p.ImagePath);
            });
        }

        private IResult CheckIfNumberOfCarPicturesByCarId(int carId)
        {
            int carImageCount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (carImageCount >= 5)
                return new ErrorResult(Messages.CarImageCountError);

            return new SuccessResult();
        }

        private IResult CheckIfCarExist(int carId)
        {
            var result = _carService.GetById(carId);
            if (!result.Success)
                return result;

            return result;
        }
    }
}
