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
        public IResult Add(ColorAddDto colorAddDto)
        {
            Color colorToAdd = new Color()
            {
                Name = colorAddDto.Name
            };

            bool addResult = _colorDal.Add(colorToAdd);

            if (!addResult)
                return new ErrorResult(Messages.ColorNotAdded);

            return new SuccessResult(Messages.ColorAdded);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            bool deleteResult = _colorDal.Delete(color);

            if (!deleteResult)
                return new ErrorResult(Messages.ColorNotDeleted);

            return new SuccessResult(Messages.ColorDeleted);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);

            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            var data = _colorDal.GetAll();

            if (data.Count == 0)
                return new ErrorDataResult<List<Color>>(data, Messages.ColorNotFound);
            else
                return new SuccessDataResult<List<Color>>(data, Messages.ColorGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<Color> GetById(int id)
        {
            var findedColor = _colorDal.Get(p => p.Id == id);

            if (findedColor == null)
                return new ErrorDataResult<Color>(null, Messages.ColorNotFound);

            return new SuccessDataResult<Color>(findedColor, Messages.ColorGet);
        }

        public IDataResult<Color> GetByName(string name)
        {
            var color = _colorDal.Get(p => p.Name.Equals(name));

            if (color == null)
                return new ErrorDataResult<Color>(null, Messages.ColorNotFound);

            return new SuccessDataResult<Color>(color, Messages.ColorGet);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(ColorUpdateDto colorUpdateDto)
        {
            var ruleResult = BusinessRules.Run(CheckColorNameExistButIgnoreById(colorUpdateDto.Id, colorUpdateDto.Name));
            if (!ruleResult.Success)
                return ruleResult;

            var findedColor = _colorDal.Get(p => p.Id == colorUpdateDto.Id);
            if (findedColor == null)
                return new ErrorResult(Messages.ColorNotFound);

            findedColor.Name = colorUpdateDto.Name;

            bool updateResult = _colorDal.Update(findedColor);

            if (!updateResult)
                return new ErrorResult(Messages.ColorNotUpdated);

            return new SuccessResult(Messages.ColorUpdated);
        }
        private IResult CheckColorNameExistButIgnoreById(int colorId, string colorName)
        {
            var findedColor = _colorDal.Get(p => p.Id != colorId && p.Name.Equals(colorName));
            if (findedColor == null)
                return new SuccessResult();

            return new ErrorResult(Messages.ColorNameAlreadyExist);
        }
    }
}