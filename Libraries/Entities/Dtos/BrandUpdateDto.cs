using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class BrandUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
