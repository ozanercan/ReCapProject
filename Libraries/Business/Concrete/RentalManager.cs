using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICustomerCreditScoreService _customerCreditScoreService;
        private readonly ICarCreditScoreService _carCreditScoreService;

        public RentalManager(IRentalDal rentalDal, ICustomerCreditScoreService customerCreditScoreService, ICarCreditScoreService carCreditScoreService)
        {
            _rentalDal = rentalDal;
            _customerCreditScoreService = customerCreditScoreService;
            _carCreditScoreService = carCreditScoreService;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [ValidationAspect(typeof(RentalAddDtoValidator))]
        [SecuredOperation("customer")]
        public async Task<IDataResult<Rental>> AddAsync(RentalAddDto rentalCreateDto)
        {
            var ruleResult = BusinessRules.Run(
                (await CheckRentDateAsync(rentalCreateDto.CarId, rentalCreateDto.RentDate)),
                CheckIfReturnDateSmallOfRentDate(rentalCreateDto.RentDate, rentalCreateDto.ReturnDate.Value),
                (await CheckCreditScoreByCustomerIdAsync(rentalCreateDto.CustomerId, rentalCreateDto.CarId)));

            if (!ruleResult.Success)
                return new ErrorDataResult<Rental>(null, ruleResult.Message);

            var rentalToAdd = new Rental()
            {
                CarId = rentalCreateDto.CarId,
                CustomerId = rentalCreateDto.CustomerId,
                RentDate = rentalCreateDto.RentDate,
                ReturnDate = rentalCreateDto.ReturnDate
            };

            bool addResult = await _rentalDal.AddAsync(rentalToAdd);

            if (addResult == true)
                return new SuccessDataResult<Rental>(rentalToAdd, Messages.RentalAdded);
            else
                return new ErrorDataResult<Rental>(null, Messages.RentalNotAdded);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Rental>>> GetAllAsync()
        {
            var data = await _rentalDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<Rental>>(data, Messages.RentalNotFound);
            else
                return new SuccessDataResult<List<Rental>>(data, Messages.RentalGetListByRegistered);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Rental>>> GetListReturnDateIsNullAsync()
        {
            var rentals = await _rentalDal.GetAllAsync(p => p.ReturnDate == null);
            if (rentals.Count == 0)
            {
                return new ErrorDataResult<List<Rental>>(null, Messages.RentalNotFound);
            }
            return new SuccessDataResult<List<Rental>>(rentals, Messages.RentalListed);
        }

        [PerformanceAspect(5)]
        public async Task<IDataResult<Rental>> GetByIdAsync(int id)
        {
            var rental = await _rentalDal.GetAsync(p => p.Id == id);
            if (rental == null)
                return new ErrorDataResult<Rental>(rental, Messages.RentalNotFound);

            return new SuccessDataResult<Rental>(rental, Messages.RentalGet);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(Rental brand)
        {
            var updateResult = await _rentalDal.UpdateAsync(brand);
            if (updateResult == false)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }
            return new SuccessResult(Messages.RentalUpdated);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Rental brand)
        {
            var deleteResult = await _rentalDal.UpdateAsync(brand);
            if (deleteResult == false)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }

            return new SuccessResult(Messages.RentalDeleted);
        }

        [CacheAspect]
        public async Task<IDataResult<List<RentalDto>>> GetAllDtoAsync()
        {
            var getResult = await _rentalDal.GetRentalDtosAsync();
            if (getResult.Count == 0)
                return new ErrorDataResult<List<RentalDto>>(null, Messages.RentalNotFound);

            return new SuccessDataResult<List<RentalDto>>(getResult, Messages.RentalListed);
        }

        public async Task<IDataResult<int?>> GetCustomerIdByIdAsync(int id)
        {
            var rental = await _rentalDal.GetAsync(p => p.Id == id);
            if (rental == null)
                return new ErrorDataResult<int?>(null, Messages.RentalNotFound);

            return new SuccessDataResult<int?>(rental.CustomerId);
        }

        private async Task<IResult> CheckRentDateAsync(int carId, DateTime rentDate)
        {
            var rentals = await _rentalDal.GetAllNoTrackingAsync(p => p.CarId == carId);

            foreach (var item in rentals)
            {
                if (rentDate < item.ReturnDate)
                {
                    return new ErrorResult(Messages.CarAlreadyRented);
                }
            }

            return new SuccessResult();
        }

        private IResult CheckIfReturnDateSmallOfRentDate(DateTime rentDate, DateTime returnDate)
        {
            if (returnDate < rentDate)
                return new ErrorResult(Messages.ReturnDateCantLessThanReturnDate);

            return new SuccessResult();
        }

        private async Task<IResult> CheckCreditScoreByCustomerIdAsync(int userId, int carId)
        {
            var creditScoreResult = _customerCreditScoreService.CalculateByCustomerId(userId);
            if (!creditScoreResult.Success)
                return creditScoreResult;

            var carMinScoreResult = await _carCreditScoreService.GetMinScoreByCarIdAsync(carId);
            if (!carMinScoreResult.Success)
                return carMinScoreResult;

            if (creditScoreResult.Data >= carMinScoreResult.Data)
                return new SuccessResult(Messages.CustomerCreditScoreEnoughtToRentCar);

            return new ErrorResult(Messages.CustomerCreditScoreNotEnoughtToRentCar);
        }
    }
}
