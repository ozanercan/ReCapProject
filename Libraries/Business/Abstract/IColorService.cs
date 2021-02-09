using Core.Business.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        IBusinessDataResult<Color> GetById(int id);

        IBusinessDataResult<List<Color>> GetAll();

        IBusinessResult Add(Color color);

        IBusinessResult Update(Color color);

        IBusinessResult Update(int id, Color newColor);

        IBusinessResult Delete(Color color);

        IBusinessResult DeleteById(int id);
    }
}