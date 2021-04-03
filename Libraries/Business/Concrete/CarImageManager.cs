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
                await CheckIfNumberOfCarPicturesByCarIdAsync(carImageAddDto.CarId));

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
        public async Task<IResult> DeleteAsync(CarImageDeleteDto carImageDeleteDto)
        {
            var carImageResult = await this.GetByIdAsync(carImageDeleteDto.Id);
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

        [CacheAspect]
        public async Task<IDataResult<List<CarImage>>> GetAllNoTrackingAsync()
        {
            var carImages = await _carImageDal.GetAllNoTrackingAsync();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            GetImagePathScheme(_httpContextAccessor.HttpContext.Request, carImages);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        [CacheAspect]
        public async Task<IDataResult<List<CarImage>>> GetAllAsync()
        {
            var carImages = await _carImageDal.GetAllNoTrackingAsync();
            return AddUrlToImage(carImages);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<CarImage>>> GetAllByCarIdAsync(int carId)
        {
            var findedCarImages = (await this.GetAllAsync()).Data.Where(p => p.CarId == carId).ToList();
            if (findedCarImages.Count == 0)
            {
                return AddUrlToDefaultImage(carId);
            }

            return new SuccessDataResult<List<CarImage>>(findedCarImages, Messages.CarImagesListed);
        }

        private IDataResult<List<CarImage>> AddUrlToDefaultImage(int carId)
        {
            var defaultCarImage = GetDefaultCarImage(carId);

            return new SuccessDataResult<List<CarImage>>(new List<CarImage> { defaultCarImage.Data });
        }
        private IDataResult<List<CarImage>> AddUrlToImage(List<CarImage> findedCarImages)
        {
            GetImagePathScheme(_httpContextAccessor.HttpContext.Request, findedCarImages);

            return new SuccessDataResult<List<CarImage>>(findedCarImages);
        }

        public async Task<IDataResult<CarImage>> GetByIdAsync(int id)
        {
            var carImage = await _carImageDal.GetAsync(p => p.Id == id);

            if (carImage == null)
                return new ErrorDataResult<CarImage>(null, Messages.CarImageNotFound);

            return new SuccessDataResult<CarImage>(carImage, Messages.CarImageBrought);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public async Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto)
        {
            var carImageResult = await this.GetByIdAsync(carImageUpdateDto.Id);
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

            var updateResult = await _carImageDal.UpdateAsync(carImageResult.Data);

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
                if (p.ImagePath.IndexOf(httpRequest.Scheme) == -1)
                {
                    p.ImagePath = string.Join(@"/", httpRequest.Scheme + ":/", httpRequest.Host.Value, p.ImagePath);
                }
            });
        }

        private async Task<IResult> CheckIfNumberOfCarPicturesByCarIdAsync(int carId)
        {
            int carImageCount = (await _carImageDal.GetAllAsync(p => p.CarId == carId)).Count;
            if (carImageCount >= 5)
                return new ErrorResult(Messages.CarImageCountError);

            return new SuccessResult();
        }
    }
}
