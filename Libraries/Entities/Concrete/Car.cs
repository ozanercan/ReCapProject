using Core.DataAccess.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        [DataType("decimal(10, 2)")]
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}