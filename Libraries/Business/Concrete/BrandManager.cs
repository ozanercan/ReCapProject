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
        [ValidationAspect(typeof(BrandAddDtoValidator))]
        [SecuredOperation("brand.add")]
        public IResult Add(BrandAddDto brandAddDto)
        {
            var ruleResult = BusinessRules.Run(CheckBrandNameExist(brandAddDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            Brand brandToAdd = new Brand()
            {
                Name = brandAddDto.Name
            };

            bool addResult = _brandDal.Add(brandToAdd);

            if (addResult == true)
                return new SuccessResult(Messages.BrandAdded);
            else
                return new ErrorResult(Messages.BrandNotAdded);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            bool deleteResult = _brandDal.Delete(brand);

            if (deleteResult == true)
                return new SuccessResult(Messages.BrandDeleted);
            else
                return new ErrorResult(Messages.BrandNotDeleted);
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

            if (data.Count == 0)
                return new ErrorDataResult<List<Brand>>(data, Messages.BrandNotFound);
            else
                return new SuccessDataResult<List<Brand>>(data, Messages.BrandGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            var findedBrand = _brandDal.Get(p => p.Id == id);

            if (findedBrand == null)
                return new ErrorDataResult<Brand>(null, Messages.BrandNotFound);

            return new SuccessDataResult<Brand>(findedBrand, Messages.BrandGet);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandUpdateDtoValidator))]
        public IResult Update(BrandUpdateDto brandUpdateDto)
        {
            var ruleResult = BusinessRules.Run(CheckBrandNameExistButIgnoreById(brandUpdateDto.Id, brandUpdateDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            var findedBrandResult = this.GetById(brandUpdateDto.Id);
            if (!findedBrandResult.Success)
                return new ErrorResult(findedBrandResult.Message);

            findedBrandResult.Data.Name = brandUpdateDto.Name;

            bool updateResult = _brandDal.Update(findedBrandResult.Data);

            if (!updateResult)
                return new ErrorResult(Messages.BrandNotUpdated);

            return new SuccessResult(Messages.BrandUpdated);
        }



        public IDataResult<Brand> GetByName(string name)
        {
            var findedBrand = _brandDal.Get(p => p.Name.Equals(name));

            if (findedBrand == null)
                return new ErrorDataResult<Brand>(null, Messages.BrandNotFound);
            else
                return new SuccessDataResult<Brand>(findedBrand, Messages.BrandGet);
        }

        private IResult CheckBrandNameExist(string brandName)
        {
            var findedBrand = _brandDal.Get(p => p.Name.Equals(brandName));
            if (findedBrand == null)
                return new SuccessResult();

            return new ErrorResult(Messages.BrandNameAlreadyExist);
        }

        private IResult CheckBrandNameExistButIgnoreById(int brandId, string brandName)
        {
            var findedBrand = _brandDal.Get(p => p.Id != brandId && p.Name.Equals(brandName));
            if (findedBrand == null)
                return new SuccessResult();

            return new ErrorResult(Messages.BrandNameAlreadyExist);
        }
    }
}