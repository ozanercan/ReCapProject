using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int FuelTypeId { get; set; }
        public int GearTypeId { get; set; }
        public int ModelYear { get; set; }
        public decimal HorsePower { get; set; }
        public string Name { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}