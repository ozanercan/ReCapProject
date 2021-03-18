using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarFilterDto : IDto
    {
        public string ColorName { get; set; }
        public string BrandName { get; set; }
    }
}
