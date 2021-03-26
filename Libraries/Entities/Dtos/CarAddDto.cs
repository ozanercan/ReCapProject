using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarAddDto : IDto
    {
        public string BrandName { get; set; }
        public string ColorName{ get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

        public int MinCreditScore { get; set; }
    }
}
