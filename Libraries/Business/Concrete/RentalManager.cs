using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            bool addResult = _rentalDal.Add(rental);

            if (addResult == true)
                return new SuccessResult(Messages.RentalAdded);
            else
                return new ErrorResult(Messages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            bool deleteResult = _rentalDal.Delete(rental);

            if (deleteResult == true)
                return new SuccessResult(Messages.RentalDeleted);
            else
                return new ErrorResult(Messages.RentalDeleted);
        }

        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);
            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            var data = _rentalDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Rental>>(data, Messages.RentalNotFound);
            else
                return new SuccessDataResult<List<Rental>>(data, Messages.RentalGetListByRegistered);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return new ErrorDataResult<Rental>(getResult.Data, Messages.RentalNotFound);

            if (getResult == null)
                return new ErrorDataResult<Rental>(getResult.Data, Messages.RentalNotFound);
            else
                return new SuccessDataResult<Rental>(getResult.Data, Messages.RentalGetListByRegistered);
        }

        public IResult Update(Rental rental)
        {
            bool updateResult = _rentalDal.Update(rental);

            if (updateResult == true)
                return new SuccessResult(Messages.RentalUpdated);
            else
                return new ErrorResult(Messages.RentalNotUpdated);
        }

        public IResult Update(int id, Rental newRental)
        {
            var findedEntityResult = GetById(id);

            if (!findedEntityResult.Success)
                return findedEntityResult;


            Rental rentalToUpdate = InputToCar(findedEntityResult.Data, newRental);

            return Update(rentalToUpdate);
        }

        private Rental InputToCar(Rental oldRental, Rental newRental)
        {
            oldRental.CarId = newRental.CarId;
            oldRental.CustomerId = newRental.CustomerId;
            oldRental.RentDate = newRental.RentDate;
            oldRental.ReturnDate = newRental.ReturnDate;

            return oldRental;
        }
    }
}
