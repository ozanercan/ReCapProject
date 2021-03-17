using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<CarImage> GetById(int id);

        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetAllNoTracking();

        IDataResult<List<CarImage>> GetAllByCarId(int carId);

        IDataResult<CarImage> GetDefaultCarImage(int carId);

        Task<IResult> AddAsync(CarImageAddDto carImageAddDto);

        Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto);

        IResult Delete(CarImageDeleteDto carImage);
        IDataResult<string> GetDefaultCarImageUrl();
        IDataResult<List<CarImage>> GetAllByCarDetails();
    }
}