using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        Car GetById(int id);

        List<Car> GetAll();

        void Add(Car car);

        void Update(Car car);

        void Update(int id, Car car);

        void Delete(Car car);

        void Delete(int id, Car car);

        void DeleteById(int id);
    }
}