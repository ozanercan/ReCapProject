using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            string description = "Fiyatlara otoyol geçişleri dahil değildir.";
            _cars = new List<Car>()
            {
                new Car(){
                    Id=1,
                    BrandId=1,
                    ColorId=1,
                    DailyPrice=129,
                    ModelYear=2010,
                    Description=description
                },
                new Car(){
                    Id=2,
                    BrandId=1,
                    ColorId=2,
                    DailyPrice=149,
                    ModelYear=2013,
                    Description=description
                }
            };
        }

        public bool Add(Car car)
        {
            bool createSuccess = false;

            _cars.Add(car);

            var result = _cars.Where(p => p.Id == car.Id);

            if (result.Any())
                createSuccess = true;

            return createSuccess;
        }

        public bool Delete(Car car)
        {
            bool deleteSuccess = false;

            Car carToDelete = _cars.Where(p => p.Id == car.Id).First();

            if (carToDelete == null)
            {
                Console.WriteLine("Selected car not found in memory.");
            }
            else
            {
                _cars.Remove(carToDelete);

                var result = _cars.Where(p => p.Id == carToDelete.Id);

                if (result.Any())
                    deleteSuccess = true;
            }

            return deleteSuccess;
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            return _cars.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Update(Car car)
        {
            bool updateSuccess = false;

            Car carToUpdate = _cars.Where(p => p.Id == car.Id).First();

            if (carToUpdate == null)
            {
                Console.WriteLine("Selected car not found in memory.");
            }
            else
            {
                carToUpdate.BrandId = car.BrandId;
                carToUpdate.ColorId = car.ColorId;
                carToUpdate.DailyPrice = car.DailyPrice;
                carToUpdate.Description = car.Description;
                carToUpdate.ModelYear = car.ModelYear;

                updateSuccess = true;
            }

            return updateSuccess;
        }
    }
}