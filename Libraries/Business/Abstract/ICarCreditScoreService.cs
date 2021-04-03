using Core.Utilities.Results;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarCreditScoreService
    {
        Task<IDataResult<int?>> GetMinScoreByCarIdAsync(int carId);
        Task<IResult> UpdateAsync(CarCreditScoreUpdateDto carCreditScoreUpdateDto);
        Task<IResult> AddAsync(CarCreditScoreAddDto carCreditScoreAddDto);
    }
}