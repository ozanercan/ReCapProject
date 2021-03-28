using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEfRepository<Car>
    {
        Task<List<CarDetailDto>> GetCarDetailsAsync();
        Task<List<CarDetailDto>> GetCarDetailsByBrandIdAsync(int brandId);
        Task<List<CarDetailDto>> GetCarDetailsByColorIdAsync(int colorId);
        Task<CarDetailDto> GetCarDetailByIdAsync(int id);
        Task<List<CarDetailDto>> GetCarDetailsByBrandNameAsync(string brandName);
        Task<List<CarDetailDto>> GetCarDetailsByColorNameAsync(string colorName);
        Task<List<CarDetailDto>> GetCarDetailsByFilterAsync(CarFilterDto carFilterDto);
    }
}