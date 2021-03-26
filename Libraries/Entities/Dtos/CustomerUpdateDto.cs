using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CustomerUpdateDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ActivePassword { get; set; }
        public string NewPassword { get; set; }
    }
}
