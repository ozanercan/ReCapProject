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
using System.Threading.Tasks;

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
        [SecuredOperation("admin")]
        public async Task<IResult> AddAsync(BrandAddDto brandAddDto)
        {
            var ruleResult = BusinessRules.Run(await CheckBrandNameExistAsync(brandAddDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            Brand brandToAdd = new Brand()
            {
                Name = brandAddDto.Name
            };

            bool addResult = await _brandDal.AddAsync(brandToAdd);

            if (addResult == true)
                return new SuccessResult(Messages.BrandAdded);
            else
                return new ErrorResult(Messages.BrandNotAdded);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Brand brand)
        {
            bool deleteResult = await _brandDal.DeleteAsync(brand);

            if (deleteResult == true)
                return new SuccessResult(Messages.BrandDeleted);
            else
                return new ErrorResult(Messages.BrandNotDeleted);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var brandGetResult = await GetByIdAsync(id);
            if (!brandGetResult.Success)
                return brandGetResult;

            return await DeleteAsync(brandGetResult.Data);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Brand>>> GetAllAsync()
        {
            var data = await _brandDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<Brand>>(data, Messages.BrandNotFound);
            else
                return new SuccessDataResult<List<Brand>>(data, Messages.BrandGetListByRegistered);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<Brand>> GetByIdAsync(int id)
        {
            var findedBrand = await _brandDal.GetAsync(p => p.Id == id);

            if (findedBrand == null)
                return new ErrorDataResult<Brand>(null, Messages.BrandNotFound);

            return new SuccessDataResult<Brand>(findedBrand, Messages.BrandGet);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandUpdateDtoValidator))]
        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(BrandUpdateDto brandUpdateDto)
        {
            var ruleResult = BusinessRules.Run(await CheckBrandNameExistButIgnoreByIdAsync(brandUpdateDto.Id, brandUpdateDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            var findedBrandResult = await this.GetByIdAsync(brandUpdateDto.Id);
            if (!findedBrandResult.Success)
                return new ErrorResult(findedBrandResult.Message);

            findedBrandResult.Data.Name = brandUpdateDto.Name;

            bool updateResult = await _brandDal.UpdateAsync(findedBrandResult.Data);

            if (!updateResult)
                return new ErrorResult(Messages.BrandNotUpdated);

            return new SuccessResult(Messages.BrandUpdated);
        }


        public async Task<IDataResult<Brand>> GetByNameAsync(string name)
        {
            var findedBrand = await _brandDal.GetAsync(p => p.Name.Equals(name));

            if (findedBrand == null)
                return new ErrorDataResult<Brand>(null, Messages.BrandNotFound);
            else
                return new SuccessDataResult<Brand>(findedBrand, Messages.BrandGet);
        }

        private async Task<IResult> CheckBrandNameExistAsync(string brandName)
        {
            var findedBrand = await _brandDal.GetAsync(p => p.Name.Equals(brandName));
            if (findedBrand == null)
                return new SuccessResult();

            return new ErrorResult(Messages.BrandNameAlreadyExist);
        }

        private async Task<IResult> CheckBrandNameExistButIgnoreByIdAsync(int brandId, string brandName)
        {
            var findedBrand = await _brandDal.GetAsync(p => p.Id != brandId && p.Name.Equals(brandName));
            if (findedBrand == null)
                return new SuccessResult();

            return new ErrorResult(Messages.BrandNameAlreadyExist);
        }
    }
}