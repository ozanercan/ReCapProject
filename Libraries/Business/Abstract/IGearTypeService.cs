using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGearTypeService
    {
        Task<IDataResult<GearType>> GetByNameAsync(string name);
        Task<IDataResult<List<GearTypeViewDto>>> GetAllViewDtos();
    }
}
