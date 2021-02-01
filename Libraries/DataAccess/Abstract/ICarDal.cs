using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ICarDal
    {
        Car GetById(int id);

        List<Car> GetAll();

        bool Add(Car car);

        bool Update(Car car);

        bool Delete(Car car);
    }
}