using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<Color> GetById(int id);

        IDataResult<List<Color>> GetAll();

        IResult Add(Color color);

        IResult Update(Color color);

        IResult Update(int id, Color newColor);

        IResult Delete(Color color);

        IResult DeleteById(int id);
    }
}