using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
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

        public void Add(Car car)
        {
            if (car.Id == 0)
                car.Id = _carDal.GetAll().Count + 1;

            bool addResult = _carDal.Add(car);

            if (addResult == true)
                Console.WriteLine("Car added in memory.");
            else
                Console.WriteLine("Car not add in memory.");
        }

        public void Delete(Car car)
        {
            bool deleteResult = _carDal.Delete(car);

            if (deleteResult == true)
                Console.WriteLine("Car deleted in memory.");
            else
                Console.WriteLine("Car not delete in memory.");
        }

        public void Delete(int id, Car car)
        {
            car.Id = id;

            Delete(car);
        }

        public void DeleteById(int id)
        {
            Car deleteToCar = _carDal.GetById(id);

            Delete(deleteToCar);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetById(int id)
        {
            return _carDal.GetById(id);
        }

        public void Update(Car car)
        {
            bool updateResult = _carDal.Update(car);

            if (updateResult == true)
                Console.WriteLine("Car updated in memory.");
            else
                Console.WriteLine("Car not update in memory.");
        }

        public void Update(int id, Car car)
        {
            car.Id = id;

            Update(car);
        }
    }
}