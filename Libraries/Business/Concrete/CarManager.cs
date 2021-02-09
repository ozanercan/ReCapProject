using Business.Abstract;
using Business.Contants;
using Core.Business.Results.Abstract;
using Core.Business.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IBusinessResult Add(Car car)
        {
            if (car.DailyPrice <= 0)
                return new BusinessResult(Messages.CarDailyPriceInvalid, false);

            if (car.Description.Length <= 2)
                return new BusinessResult(Messages.CarDescriptionInvalid, false);

            bool addResult = _carDal.Add(car);

            string message;
            if (addResult == true)
                message = Messages.CarAdded;
            else
                message = Messages.CarNotAdded;

            return new BusinessResult(message, addResult);
        }

        public IBusinessResult Delete(Car car)
        {
            bool deleteResult = _carDal.Delete(car);

            string message;
            if (deleteResult == true)
                message = Messages.CarDeleted;
            else
                message = Messages.CarNotAdded;

            return new BusinessResult(message, deleteResult);
        }

        public IBusinessResult DeleteById(int id)
        {
            Car deleteToCar = _carDal.GetById(id);

            return Delete(deleteToCar);
        }

        public IBusinessDataResult<List<Car>> GetAll()
        {
            var data = _carDal.GetAll();

            string message;
            bool isSuccess;
            if (data == null || data.Count <= 0)
            {
                message = Messages.CarNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.CarGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<List<Car>>(message, isSuccess, data);
        }

        public IBusinessDataResult<Car> GetById(int id)
        {
            string message = "";
            bool isSuccess = false;

            var data = _carDal.Get(p => p.Id == id);

            if (data == null)
            {
                message = Messages.CarNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.CarGet;
                isSuccess = true;
            }

            return new BusinessDataResult<Car>(message, isSuccess, data);
        }

        public IBusinessDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var data = _carDal.GetCarDetails();

            string message;
            bool isSuccess;
            if (data == null || data.Count <= 0)
            {
                message = Messages.CarNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.CarGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<List<CarDetailDto>>(message, isSuccess, data);
        }

        public IBusinessDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            string message = "";
            bool isSuccess = false;

            var data = _carDal.GetAll(p => p.BrandId == brandId);

            if (data == null || data.Count <= 0)
            {
                message = Messages.CarNotFoundByBrand;
                isSuccess = false;
            }
            else
            {
                message = Messages.CarGetListByBrand;
                isSuccess = true;
            }

            return new BusinessDataResult<List<Car>>(message, isSuccess, data);
        }

        public IBusinessDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            string message = "";
            bool isSuccess = false;

            var data = _carDal.GetAll(p => p.ColorId == colorId);

            if (data == null || data.Count <= 0)
            {
                message = Messages.CarNotFoundByColor;
                isSuccess = false;
            }
            else
            {
                message = Messages.CarGetListByColor;
                isSuccess = true;
            }

            return new BusinessDataResult<List<Car>>(message, isSuccess, data);
        }

        public IBusinessResult Update(Car car)
        {
            bool updateResult = _carDal.Update(car);

            string message;
            if (updateResult == true)
                message = Messages.CarUpdated;
            else
                message = Messages.CarNotUpdated;

            return new BusinessResult(message, updateResult);
        }

        public IBusinessResult Update(int id, Car newCar)
        {
            var findedCarResult = GetById(id);

            Car updatedCar = InputToCar(findedCarResult.Data, newCar);

            return Update(updatedCar);
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