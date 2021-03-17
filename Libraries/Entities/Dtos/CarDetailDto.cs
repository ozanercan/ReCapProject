using Core.Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class CarDetailDto : IDto
    {
        public CarDetailDto()
        {
            ImagePaths = new List<CarImage>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

        public List<CarImage> ImagePaths { get; set; }
    }
}