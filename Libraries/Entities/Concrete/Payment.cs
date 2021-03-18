﻿using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Payment:IEntity
    {
        public int Id { get; set; }
        public int RentId { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
    }
}
