using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CustomerCreateDto : IDto
    {
        public string CompanyName { get; set; }
    }
}
