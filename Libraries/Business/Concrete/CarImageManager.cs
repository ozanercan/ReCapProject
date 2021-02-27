using Business.Abstract;
using Business.Constants;
using Business.Utilities.FileHelper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageAddDtoValidator))]
        public async Task<IResult> AddAsync(CarImageAddDto carImageAddDto, IHostEnvironment hostEnvironment)
        {
            FileHelper.Initialize(hostEnvironment);

            var uploadResult = await FileHelper.ImageUploadAsync(carImageAddDto.FormFile);
            if (!uploadResult.Success)
                return new ErrorResult("Resim yüklenemedi.");

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
                return new ErrorResult("Kayıtlı resim silinemedi.");

            bool deleteResult = _carImageDal.Delete(carImageResult.Data);

            if (!deleteResult)
                return new ErrorResult(Messages.CarImageNotDeleted);

            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var carImages = _carImageDal.GetAll();

            if (carImages == null)
                return new ErrorDataResult<List<CarImage>>(null, Messages.CarImagesNotFound);

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
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

            var carImageResult = this.GetById(carImageUpdateDto.Id);
            if (!carImageResult.Success)
                return new ErrorResult(carImageResult.Message);

            var fileRemoveResult = FileHelper.FileRemove(carImageResult.Data.ImagePath);

            if (!fileRemoveResult.Success)
                return new ErrorResult("Kayıtlı resim silinemedi.");

            var fileAddResult = await FileHelper.ImageUploadAsync(carImageUpdateDto.FormFile);

            if (!fileAddResult.Success)
                return new ErrorResult("Resim yüklenemedi.");

            carImageResult.Data.CarId = carImageUpdateDto.CarId;
            carImageResult.Data.ImagePath = fileAddResult.ShortPath;

            var updateResult = _carImageDal.Update(carImageResult.Data);

            if (!updateResult)
                return new ErrorResult(Messages.CarImageNotUpdated);

            return new SuccessResult(Messages.CarImageUpdated);
        }

    }
}
