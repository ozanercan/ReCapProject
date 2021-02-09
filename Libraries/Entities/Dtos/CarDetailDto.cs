using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarDetailDto : IDto
    {
        public string CarDescription { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
    }
}