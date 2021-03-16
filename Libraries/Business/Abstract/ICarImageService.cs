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

        IDataResult<List<CarImage>> GetAll(HttpRequest httpRequest);

        IDataResult<List<CarImage>> GetAllByCarId(int carId, HttpRequest httpRequest);

        Task<IResult> AddAsync(CarImageAddDto carImageAddDto, IHostEnvironment hostEnvironment);

        Task<IResult> UpdateAsync(CarImageUpdateDto carImageUpdateDto, IHostEnvironment hostEnvironment);

        IResult Delete(CarImageDeleteDto carImage, IHostEnvironment hostEnvironment);
        IDataResult<string> GetDefaultCarImageUrl();
    }
}