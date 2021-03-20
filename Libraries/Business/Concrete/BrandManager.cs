﻿using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            bool addResult = _brandDal.Add(brand);

            if (addResult == true)
                return new SuccessResult(Messages.BrandAdded);
            else
                return new ErrorResult(Messages.BrandAdded);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            bool deleteResult = _brandDal.Delete(brand);

            if (deleteResult == true)
                return new SuccessResult(Messages.BrandDeleted);
            else
                return new ErrorResult(Messages.BrandDeleted);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult DeleteById(int id)
        {
            var brandGetResult = GetById(id);
            if (!brandGetResult.Success)
                return brandGetResult;

            return Delete(brandGetResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            var data = _brandDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Brand>>(data, Messages.BrandNotFound);
            else
                return new SuccessDataResult<List<Brand>>(data, Messages.BrandGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return new ErrorDataResult<Brand>(getResult.Data, Messages.BrandNotFound);

            if (getResult == null)
                return new ErrorDataResult<Brand>(getResult.Data, Messages.BrandNotFound);
            else
                return new SuccessDataResult<Brand>(getResult.Data, Messages.BrandGetListByRegistered);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            bool updateResult = _brandDal.Update(brand);

            if (updateResult == true)
                return new SuccessResult(Messages.BrandUpdated);
            else
                return new ErrorResult(Messages.BrandNotUpdated);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(int id, Brand newBrand)
        {
            var findedCarResult = GetById(id);

            if (!findedCarResult.Success)
                return findedCarResult;


            Brand brandToUpdate = InputToCar(findedCarResult.Data, newBrand);

            return Update(brandToUpdate);
        }

        private Brand InputToCar(Brand oldBrand, Brand newBrand)
        {
            oldBrand.Name = newBrand.Name;
            return oldBrand;
        }
    }
}