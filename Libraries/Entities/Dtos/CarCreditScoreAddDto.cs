using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarCreditScoreAddDto:IDto
    {
        public int CarId { get; set; }
        public int MinCreditScore { get; set; }
    }
}
