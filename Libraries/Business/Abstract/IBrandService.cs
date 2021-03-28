using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<IDataResult<Brand>> GetByIdAsync(int id);

        Task<IDataResult<Brand>> GetByNameAsync(string name);

        Task<IDataResult<List<Brand>>> GetAllAsync();

        Task<IResult> AddAsync(BrandAddDto brandAddDto);

        Task<IResult> UpdateAsync(BrandUpdateDto brandUpdateDto);

        Task<IResult> DeleteAsync(Brand brand);

        Task<IResult> DeleteByIdAsync(int id);
    }
}