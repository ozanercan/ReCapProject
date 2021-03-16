using Core.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator List<object>(CarImage v)
        {
            throw new NotImplementedException();
        }
    }
}
