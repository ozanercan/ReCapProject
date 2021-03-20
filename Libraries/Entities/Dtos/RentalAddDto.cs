using Core.Entities.Abstract;
using System;

namespace Entities.Dtos
{
    public class RentalAddDto : IDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
