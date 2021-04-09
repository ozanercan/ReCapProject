using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IDataResult<CarImage>> GetByIdAsync(int id);

        Task<IDataResult<List<CarImage>>> GetAllAsync();

        Task<IDataResult<List<CarImage>>> GetAllNoTrackingAsync();

        Task<IDataResult<List<CarImage>>> GetAllByCarIdAsync(int carId);

        IDataResult<CarImage> GetDefaultCarImage(int carId);

        Task<IResult> AddAsync(CarImageAddDto carImageAddDto);

        Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto);

        Task<IResult> DeleteAsync(CarImageDeleteDto carImage);
        Task<IResult> DeleteByIdAsync(int id);

        IDataResult<string> GetDefaultCarImageUrl();

    }
}