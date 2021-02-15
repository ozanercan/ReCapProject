using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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

        /// <summary>
        /// Aracın stokta olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns>Success True dönerse araç stoktadır, False ise araç stokta değildir.</returns>
        public IResult CheckVehicle(int carId)
        {
            var rentResult = _rentalDal.Get(p => p.Id == carId && p.ReturnDate == null);

            if (rentResult == null)
                return new SuccessResult(Messages.CarInStock);
            else
                return new ErrorResult(Messages.CarNotInStock);
        }

        public IResult Add(RentalCreateDto rentalCreateDto)
        {
            if (!CheckVehicle(rentalCreateDto.CarId).Success)
                return new ErrorResult(Messages.CarNotInStock);

            var rentalToAdd = new Rental()
            {
                CarId = rentalCreateDto.CarId,
                CustomerId = rentalCreateDto.CustomerId,
                RentDate = rentalCreateDto.RentDate,
                ReturnDate = null
            };

            bool addResult = _rentalDal.Add(rentalToAdd);

            if (addResult == true)
                return new SuccessResult(Messages.RentalAdded);
            else
                return new ErrorResult(Messages.RentalNotAdded);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            var data = _rentalDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Rental>>(data, Messages.RentalNotFound);
            else
                return new SuccessDataResult<List<Rental>>(data, Messages.RentalGetListByRegistered);
        }

        private Rental InputToCar(Rental oldRental, Rental newRental)
        {
            oldRental.CarId = newRental.CarId;
            oldRental.CustomerId = newRental.CustomerId;
            oldRental.RentDate = newRental.RentDate;
            oldRental.ReturnDate = newRental.ReturnDate;

            return oldRental;
        }

        public IDataResult<List<Rental>> GetListReturnDateIsNull()
        {
            var rentals = _rentalDal.GetAll(p => p.ReturnDate == null);
            if (rentals == null || rentals.Count == 0)
            {
                return new ErrorDataResult<List<Rental>>(null, Messages.RentalNotFound);
            }
            return new SuccessDataResult<List<Rental>>(rentals, Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var rental = _rentalDal.Get(p => p.Id == id);
            if (rental == null)
            {
                return new ErrorDataResult<Rental>(rental, Messages.RentalNotFound);
            }

            return new SuccessDataResult<Rental>(rental, Messages.RentalGet);
        }

        public IResult Update(Rental brand)
        {
            var updateResult = _rentalDal.Update(brand);
            if (updateResult == false)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental brand)
        {
            var deleteResult = _rentalDal.Update(brand);
            if (deleteResult == false)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }

            return new SuccessResult(Messages.RentalDeleted);
        }
    }
}
