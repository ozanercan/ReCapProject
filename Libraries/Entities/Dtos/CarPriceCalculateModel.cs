using System;

namespace Entities.Dtos
{
    public class CarPriceCalculateModel
    {
        public decimal DailyPrice { get; set; }
        public DateTime RentDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
