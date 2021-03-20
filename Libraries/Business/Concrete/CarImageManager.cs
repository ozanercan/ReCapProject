using Business.Abstract;
using Business.Constants;
using Business.Utilities.FileHelper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CarImageManager(ICarImageDal carImageDal, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _carImageDal = carImageDal;
            _hostEnvironment = hostEnvironment;

            FileHelper.Initialize(_hostEnvironment);
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(CarImageAddDtoValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public async Task<IResult> AddAsync(CarImageAddDto carImageAddDto)
        {
            var logicResult = BusinessRules.Run(
                CheckIfNumberOfCarPicturesByCarId(carImageAddDto.CarId));

            if (!logicResult.Success)
                return logicResult;


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

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImageDeleteDto carImageDeleteDto)
        {
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

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            var carImages = _carImageDal.GetAll();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            GetImagePathScheme(_httpContextAccessor.HttpContext.Request, carImages);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllNoTracking()
        {
            var carImages = _carImageDal.GetAllNoTracking();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            GetImagePathScheme(_httpContextAccessor.HttpContext.Request, carImages);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllByCarDetails()
        {
            var carImages = _carImageDal.GetAllNoTracking();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            GetImagePathScheme(_httpContextAccessor.HttpContext.Request, carImages);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var getCarList = _carImageDal.GetAllNoTracking(p => p.CarId == carId);

            if (getCarList.Count == 0)
            {
                var defaultCarImage = GetDefaultCarImage(carId);

                return new SuccessDataResult<List<CarImage>>(new List<CarImage> { defaultCarImage.Data });
            }
            else
            {
                GetImagePathScheme(_httpContextAccessor.HttpContext.Request, getCarList);

                return new SuccessDataResult<List<CarImage>>(getCarList);
            }
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var carImage = _carImageDal.Get(p => p.Id == id);

            if (carImage == null)
                return new ErrorDataResult<CarImage>(null, Messages.CarImageNotFound);

            return new SuccessDataResult<CarImage>(carImage, Messages.CarImageBrought);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public async Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto)
        {
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

        public IDataResult<CarImage> GetDefaultCarImage(int carId)
        {
            return new SuccessDataResult<CarImage>(new CarImage()
            {
                CarId = carId,
                ImagePath = DefaultValues.DefaultCarImageUrl
            });
        }

        public IDataResult<string> GetDefaultCarImageUrl()
        {
            return new SuccessDataResult<string>(DefaultValues.DefaultCarImageUrl);
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
    }
}
