using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarCreditScoreManager : ICarCreditScoreService
    {
        private readonly ICarCreditScoreDal _carCreditScoreDal;

        public CarCreditScoreManager(ICarCreditScoreDal carCreditScoreDal)
        {
            _carCreditScoreDal = carCreditScoreDal;
        }

        [ValidationAspect(typeof(CarCreditScoreAddDtoValidator))]
        public IResult Add(CarCreditScoreAddDto carCreditScoreAddDto)
        {
            CarCreditScore carCreditScoreToAdd = new CarCreditScore()
            {
                CarId = carCreditScoreAddDto.CarId,
                MinCreditScore = carCreditScoreAddDto.MinCreditScore
            };

            bool addResult = _carCreditScoreDal.Add(carCreditScoreToAdd);
            if (!addResult)
                return new ErrorResult(Messages.CarCreditScoreNotAdded);

            return new SuccessResult(Messages.CarCreditScoreAdded);
        }

        public IDataResult<int?> GetMinScoreByCarId(int carId)
        {
            var findedEntity = _carCreditScoreDal.Get(p => p.CarId == carId);
            if (findedEntity == null)
                return new ErrorDataResult<int?>(null, Messages.CarCreditScoreNotFound);

            return new SuccessDataResult<int?>(findedEntity.MinCreditScore, Messages.CarCreditScoreBrought);
        }
    }
}
