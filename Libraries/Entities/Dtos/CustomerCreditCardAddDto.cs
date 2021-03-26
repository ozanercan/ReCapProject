using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class CustomerCreditCardAddDto : IDto
    {
        public int UserId { get; set; }
        public string CardOwnerFullName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
    }
}
