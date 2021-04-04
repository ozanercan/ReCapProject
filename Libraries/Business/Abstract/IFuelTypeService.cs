using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFuelTypeService
    {
        Task<IDataResult<FuelType>> GetByNameAsync(string name);
        Task<IDataResult<List<FuelTypeViewDto>>> GetAllViewDtos();
    }
}
