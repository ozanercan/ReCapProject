using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
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

        [CacheRemoveAspect("IRentalService.Get")]
        public IDataResult<Rental> Add(RentalCreateDto rentalCreateDto)
        {
            if (!CheckDate(rentalCreateDto.CarId, rentalCreateDto.RentDate).Success)
                return new ErrorDataResult<Rental>(null, Messages.CarAlreadyRented);

            var rentalToAdd = new Rental()
            {
                CarId = rentalCreateDto.CarId,
                CustomerId = rentalCreateDto.CustomerId,
                RentDate = rentalCreateDto.RentDate,
                ReturnDate = rentalCreateDto.ReturnDate
            };

            bool addResult = _rentalDal.Add(rentalToAdd);

            if (addResult == true)
                return new SuccessDataResult<Rental>(rentalToAdd, Messages.RentalAdded);
            else
                return new ErrorDataResult<Rental>(null, Messages.RentalNotAdded);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
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

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Rental>> GetListReturnDateIsNull()
        {
            var rentals = _rentalDal.GetAll(p => p.ReturnDate == null);
            if (rentals == null || rentals.Count == 0)
            {
                return new ErrorDataResult<List<Rental>>(null, Messages.RentalNotFound);
            }
            return new SuccessDataResult<List<Rental>>(rentals, Messages.RentalListed);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            var rental = _rentalDal.Get(p => p.Id == id);
            if (rental == null)
            {
                return new ErrorDataResult<Rental>(rental, Messages.RentalNotFound);
            }

            return new SuccessDataResult<Rental>(rental, Messages.RentalGet);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental brand)
        {
            var updateResult = _rentalDal.Update(brand);
            if (updateResult == false)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }
            return new SuccessResult(Messages.RentalUpdated);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental brand)
        {
            var deleteResult = _rentalDal.Update(brand);
            if (deleteResult == false)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }

            return new SuccessResult(Messages.RentalDeleted);
        }


        public IDataResult<List<RentalDto>> GetAllDto()
        {
            var getResult = _rentalDal.GetRentalDtos();
            if (getResult.Count == 0)
                return new ErrorDataResult<List<RentalDto>>(null, Messages.RentalNotFound);

            return new SuccessDataResult<List<RentalDto>>(getResult, Messages.RentalListed);
        }

        private IResult CheckDate(int carId, DateTime rentDate)
        {
            var rentals = _rentalDal.GetAllNoTracking(p => p.CarId == carId);

            foreach (var item in rentals)
            {
                if (rentDate < item.ReturnDate)
                {
                    return new ErrorResult();
                }
            }

            return new SuccessResult();
        }


    }
}
