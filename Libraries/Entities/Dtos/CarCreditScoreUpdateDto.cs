using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarCreditScoreUpdateDto : IDto
    {
        public int CarId { get; set; }
        public int MinCreditScore { get; set; }
    }
}
