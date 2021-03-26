using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CustomerCreditCard : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardOwnerFullName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
    }
}
