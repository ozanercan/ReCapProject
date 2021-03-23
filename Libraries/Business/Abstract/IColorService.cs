using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<Color> GetById(int id);
        IDataResult<Color> GetByName(string name);

        IDataResult<List<Color>> GetAll();

        IResult Add(ColorAddDto colorAddDto);

        IResult Update(ColorUpdateDto colorUpdateDto);

        IResult Delete(Color color);

        IResult DeleteById(int id);
    }
}