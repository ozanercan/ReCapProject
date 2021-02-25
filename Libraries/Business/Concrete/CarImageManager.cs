using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;

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
        public IResult Add(CarImageAddDto carImageAddDto)
        {
            CarImage carImage = new CarImage()
            {
                CarId = carImageAddDto.CarId,
                ImagePath = carImageAddDto.ImagePath,
                Date = DateTime.Now
            };

            var addResult = _carImageDal.Add(carImage);

            if (!addResult)
                return new ErrorResult(Messages.CarImageNotAdded);

            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImageDeleteDto carImageDeleteDto)
        {
            var findedEntityResult = this.GetById(carImageDeleteDto.Id);

            if (!findedEntityResult.Success)
                return findedEntityResult;

            var deleteResult = _carImageDal.Delete(findedEntityResult.Data);

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

        public IResult Update(CarImageUpdateDto carImageUpdateDto)
        {
            var carImageToUpdateResult = this.GetById(carImageUpdateDto.Id);

            if (!carImageToUpdateResult.Success)
                return carImageToUpdateResult;

            carImageToUpdateResult.Data.CarId = carImageUpdateDto.CarId;
            carImageToUpdateResult.Data.ImagePath = carImageUpdateDto.ImagePath;

            var updateResult = _carImageDal.Update(carImageToUpdateResult.Data);

            if (!updateResult)
                return new ErrorResult(Messages.CarImageNotUpdated);

            return new SuccessResult(Messages.CarImageUpdated);
        }
    }
}
