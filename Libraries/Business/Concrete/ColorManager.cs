using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public IResult Add(Color color)
        {
            bool addResult = _colorDal.Add(color);

            if (addResult == true)
                return new SuccessResult(Messages.ColorAdded);
            else
                return new ErrorResult(Messages.ColorNotAdded);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            bool deleteResult = _colorDal.Delete(color);

            if (deleteResult == true)
                return new SuccessResult(Messages.ColorDeleted);
            else
                return new ErrorResult(Messages.ColorNotDeleted);
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
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            var data = _colorDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Color>>(data, Messages.ColorNotFound);
            else
                return new SuccessDataResult<List<Color>>(data, Messages.ColorGetListByRegistered);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<Color> GetById(int id)
        {
            var dataResult = this.GetById(id);

            if (!dataResult.Success)
                return dataResult;

            if (dataResult == null)
                return new ErrorDataResult<Color>(dataResult.Data, Messages.ColorNotFound);
            else
                return new SuccessDataResult<Color>(dataResult.Data, Messages.ColorGetListByRegistered);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color brand)
        {
            bool updateResult = _colorDal.Update(brand);

            if (updateResult == true)
                return new SuccessResult(Messages.ColorUpdated);
            else
                return new ErrorResult(Messages.ColorNotUpdated);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(int id, Color newColor)
        {
            var findedEntityResult = GetById(id);
            if (!findedEntityResult.Success)
                return findedEntityResult;

            Color colorToUpdate = InputToCar(findedEntityResult.Data, newColor);

            return Update(colorToUpdate);
        }

        private Color InputToCar(Color oldColor, Color newColor)
        {
            oldColor.Name = newColor.Name;
            return oldColor;
        }
    }
}