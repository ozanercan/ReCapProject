using Business.Abstract;
using Business.Contants;
using Core.Business.Results.Abstract;
using Core.Business.Results.Concrete;
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

        public IBusinessResult Add(Color color)
        {
            bool addResult = _colorDal.Add(color);

            string message;
            if (addResult == true)
                message = Messages.ColorAdded;
            else
                message = Messages.ColorNotAdded;

            return new BusinessResult(message, addResult);
        }

        public IBusinessResult Delete(Color color)
        {
            bool deleteResult = _colorDal.Delete(color);

            string message;
            if (deleteResult == true)
                message = Messages.ColorDeleted;
            else
                message = Messages.ColorNotDeleted;

            return new BusinessResult(message, deleteResult);
        }

        public IBusinessResult DeleteById(int id)
        {
            var colorToDelete = GetById(id);
            return Delete(colorToDelete.Data);
        }

        public IBusinessDataResult<List<Color>> GetAll()
        {
            var data = _colorDal.GetAll();

            string message;
            bool isSuccess;
            if (data == null || data.Count <= 0)
            {
                message = Messages.ColorNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.ColorGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<List<Color>>(message, isSuccess, data);
        }

        public IBusinessDataResult<Color> GetById(int id)
        {
            var data = _colorDal.GetById(id);

            string message;
            bool isSuccess;
            if (data == null)
            {
                message = Messages.ColorNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.ColorGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<Color>(message, isSuccess, data);
        }

        public IBusinessResult Update(Color brand)
        {
            bool updateResult = _colorDal.Update(brand);

            string message;
            if (updateResult == true)
                message = Messages.ColorUpdated;
            else
                message = Messages.ColorNotUpdated;

            return new BusinessResult(message, updateResult);
        }

        public IBusinessResult Update(int id, Color newColor)
        {
            var findedColorResult = GetById(id);

            Color updatedColor = InputToCar(findedColorResult.Data, newColor);

            return Update(updatedColor);
        }

        private Color InputToCar(Color oldColor, Color newColor)
        {
            oldColor.Name = newColor.Name;
            return oldColor;
        }
    }
}