﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarCreditScoreManager : ICarCreditScoreService
    {
        private readonly ICarCreditScoreDal _carCreditScoreDal;

        public CarCreditScoreManager(ICarCreditScoreDal carCreditScoreDal)
        {
            _carCreditScoreDal = carCreditScoreDal;
        }

        [CacheRemoveAspect("ICarCreditScoreService.Get")]
        [ValidationAspect(typeof(CarCreditScoreAddDtoValidator))]
        [SecuredOperation("admin")]
        public async Task<IResult> AddAsync(CarCreditScoreAddDto carCreditScoreAddDto)
        {
            CarCreditScore carCreditScoreToAdd = new CarCreditScore()
            {
                CarId = carCreditScoreAddDto.CarId,
                MinCreditScore = carCreditScoreAddDto.MinCreditScore
            };

            bool addResult = await _carCreditScoreDal.AddAsync(carCreditScoreToAdd);
            if (!addResult)
                return new ErrorResult(Messages.CarCreditScoreNotAdded);

            return new SuccessResult(Messages.CarCreditScoreAdded);
        }

        public async Task<IDataResult<int?>> GetMinScoreByCarIdAsync(int carId)
        {
            var findedEntity = await _carCreditScoreDal.GetAsync(p => p.CarId == carId);
            if (findedEntity == null)
                return new ErrorDataResult<int?>(null, Messages.CarCreditScoreNotFound);

            return new SuccessDataResult<int?>(findedEntity.MinCreditScore, Messages.CarCreditScoreBrought);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICarCreditScoreService.Get")]
        public async Task<IResult> UpdateAsync(CarCreditScoreUpdateDto carCreditScoreUpdateDto)
        {
            var carCreditScore = await _carCreditScoreDal.GetAsync(p => p.CarId == carCreditScoreUpdateDto.CarId);
            if (carCreditScore == null)
            {
                var addResult = await this.AddAsync(new CarCreditScoreAddDto() { CarId = carCreditScoreUpdateDto.CarId, MinCreditScore = carCreditScoreUpdateDto.MinCreditScore });
                return addResult;
            }
            carCreditScore.MinCreditScore = carCreditScoreUpdateDto.MinCreditScore;

            bool updateResult = await _carCreditScoreDal.UpdateAsync(carCreditScore);
            if (!updateResult)
                return new ErrorResult(Messages.CarCreditScoreNotUpdated);

            return new SuccessResult(Messages.CarCreditScoreUpdated);
        }
    }
}
