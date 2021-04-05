using AutoFilterer.Types;
using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CarFilterDto : FilterBase, IDto
    {
        public string? ColorName { get; set; }
        public string? BrandName { get; set; }
        public string? FuelTypeName { get; set; }
        public string? GearTypeName { get; set; }
    }
}
