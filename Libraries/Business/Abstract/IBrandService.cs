using Core.Business.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IBusinessDataResult<Brand> GetById(int id);

        IBusinessDataResult<List<Brand>> GetAll();

        IBusinessResult Add(Brand brand);

        IBusinessResult Update(Brand brand);

        IBusinessResult Update(int id, Brand newBrand);

        IBusinessResult Delete(Brand brand);

        IBusinessResult DeleteById(int id);
    }
}