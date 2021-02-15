using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IRentalService _rentalService;

        public CarManager(ICarDal carDal, IRentalService rentalService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
        }

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

        public IResult Delete(Car car)
        {
            bool deleteResult = _carDal.Delete(car);

            if (deleteResult == true)
                return new SuccessResult(Messages.CarDeleted);
            else
                return new ErrorResult(Messages.CarNotAdded);
        }

        public IResult DeleteById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        public IDataResult<List<Car>> GetAll()
        {
            var data = _carDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFound);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByRegistered);
        }

        public IDataResult<Car> GetById(int id)
        {
            var data = _carDal.Get(p => p.Id == id);

            if (data == null)
                return new ErrorDataResult<Car>(data, Messages.CarNotFound);
            else
                return new SuccessDataResult<Car>(data, Messages.CarGet);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var data = _carDal.GetCarDetails();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<CarDetailDto>>(data, Messages.CarNotFound);
            else
                return new SuccessDataResult<List<CarDetailDto>>(data, Messages.CarGetListByRegistered);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            var data = _carDal.GetAll(p => p.BrandId == brandId);

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFoundByBrand);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByBrand);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var data = _carDal.GetAll(p => p.ColorId == colorId);

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Car>>(data, Messages.CarNotFoundByColor);
            else
                return new SuccessDataResult<List<Car>>(data, Messages.CarGetListByColor);
        }

        public IDataResult<List<Car>> GetRentalCars()
        {
            var rentalResult = _rentalService.GetListReturnDateIsNull();

            var cars = _carDal.GetAll();

            foreach (var rental in rentalResult.Data)
                cars.Remove(cars.Where(p => p.Id == rental.CarId).First());

            return new SuccessDataResult<List<Car>>(cars);
        }

        public IResult Update(Car car)
        {
            bool updateResult = _carDal.Update(car);

            if (updateResult == true)
                return new SuccessResult(Messages.CarUpdated);
            else
                return new ErrorResult(Messages.CarNotUpdated);
        }

        public IResult Update(int id, Car newCar)
        {
            var findedEntityResult = GetById(id);

            if (!findedEntityResult.Success)
                return findedEntityResult;

            Car carToUpdate = InputToCar(findedEntityResult.Data, newCar);

            return Update(carToUpdate);
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