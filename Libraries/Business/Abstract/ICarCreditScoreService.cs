using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICarCreditScoreService
    {
        IDataResult<int?> GetMinScoreByCarId(int carId);
        IResult Add(CarCreditScoreAddDto carCreditScoreAddDto);
    }
}