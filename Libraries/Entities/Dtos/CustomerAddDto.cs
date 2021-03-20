using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CustomerAddDto : IDto
    {
        public string CompanyName { get; set; }
    }
}
