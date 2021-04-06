using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CarPriceCalculateDto : IDto
    {
        public int CarId { get; set; }
        public DateTime RentDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
    }
}
