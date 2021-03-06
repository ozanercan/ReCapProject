﻿using Core.Entities.Abstract;
using System;

namespace Entities.Dtos
{
    public class RentalDto : IDto
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string Customer { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Price { get; set; }
        public bool isPaid { get; set; }
    }
}
