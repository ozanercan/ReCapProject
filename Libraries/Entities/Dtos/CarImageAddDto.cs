using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class CarImageAddDto : IDto
    {
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
