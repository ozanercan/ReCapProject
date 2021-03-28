using Business.Abstract;
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
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorAddDtoValidator))]
        public async Task<IResult> AddAsync(ColorAddDto colorAddDto)
        {
            Color colorToAdd = new Color()
            {
                Name = colorAddDto.Name
            };

            bool addResult = await _colorDal.AddAsync(colorToAdd);

            if (!addResult)
                return new ErrorResult(Messages.ColorNotAdded);

            return new SuccessResult(Messages.ColorAdded);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public async Task<IResult> DeleteAsync(Color color)
        {
            bool deleteResult = await _colorDal.DeleteAsync(color);

            if (!deleteResult)
                return new ErrorResult(Messages.ColorNotDeleted);

            return new SuccessResult(Messages.ColorDeleted);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var getResult = await GetByIdAsync(id);

            if (!getResult.Success)
                return getResult;

            return await DeleteAsync(getResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<List<Color>>> GetAllAsync()
        {
            var data = await _colorDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<Color>>(data, Messages.ColorNotFound);
            else
                return new SuccessDataResult<List<Color>>(data, Messages.ColorGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<Color>> GetByIdAsync(int id)
        {
            var findedColor = await _colorDal.GetAsync(p => p.Id == id);

            if (findedColor == null)
                return new ErrorDataResult<Color>(null, Messages.ColorNotFound);

            return new SuccessDataResult<Color>(findedColor, Messages.ColorGet);
        }

        public async Task<IDataResult<Color>> GetByNameAsync(string name)
        {
            var color = await _colorDal.GetAsync(p => p.Name.Equals(name));

            if (color == null)
                return new ErrorDataResult<Color>(null, Messages.ColorNotFound);

            return new SuccessDataResult<Color>(color, Messages.ColorGet);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public async Task<IResult> UpdateAsync(ColorUpdateDto colorUpdateDto)
        {
            var ruleResult = BusinessRules.Run(await CheckColorNameExistButIgnoreByIdAsync(colorUpdateDto.Id, colorUpdateDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            var findedColor = await _colorDal.GetAsync(p => p.Id == colorUpdateDto.Id);
            if (findedColor == null)
                return new ErrorResult(Messages.ColorNotFound);

            findedColor.Name = colorUpdateDto.Name;

            bool updateResult = await _colorDal.UpdateAsync(findedColor);

            if (!updateResult)
                return new ErrorResult(Messages.ColorNotUpdated);

            return new SuccessResult(Messages.ColorUpdated);
        }
        private async Task<IResult> CheckColorNameExistButIgnoreByIdAsync(int colorId, string colorName)
        {
            var findedColor = await _colorDal.GetAsync(p => p.Id != colorId && p.Name.Equals(colorName));
            if (findedColor == null)
                return new SuccessResult();

            return new ErrorResult(Messages.ColorNameAlreadyExist);
        }
    }
}