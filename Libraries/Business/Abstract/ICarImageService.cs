using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<CarImage> GetById(int id);

        IDataResult<List<CarImage>> GetAll();

        IResult Add(CarImageAddDto carImageAddDto);

        IResult Update(CarImageUpdateDto carImageUpdateDto);

        IResult Delete(CarImageDeleteDto carImage);

    }
}