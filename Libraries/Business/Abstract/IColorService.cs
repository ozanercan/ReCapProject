using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        Task<IDataResult<Color>> GetByIdAsync(int id);

        Task<IDataResult<Color>> GetByNameAsync(string name);

        Task<IDataResult<List<Color>>> GetAllAsync();

        Task<IResult> AddAsync(ColorAddDto colorAddDto);

        Task<IResult> UpdateAsync(ColorUpdateDto colorUpdateDto);

        Task<IResult> DeleteAsync(Color color);

        Task<IResult> DeleteByIdAsync(int id);
    }
}