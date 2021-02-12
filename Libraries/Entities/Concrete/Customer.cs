using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
