using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<Brand> GetById(int id);

        IDataResult<List<Brand>> GetAll();

        IResult Add(BrandAddDto brandAddDto);

        IResult Update(Brand brand);

        IResult Update(int id, Brand newBrand);

        IResult Delete(Brand brand);

        IResult DeleteById(int id);
    }
}