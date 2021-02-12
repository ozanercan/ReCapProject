using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetById(int id);

        IDataResult<List<Rental>> GetAll();

        IResult Add(Rental rental);

        IResult Update(Rental rental);

        IResult Update(int id, Rental newRental);

        IResult Delete(Rental rental);

        IResult DeleteById(int id);
    }
}