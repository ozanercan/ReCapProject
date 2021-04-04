using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CustomerAddDto : IDto
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
