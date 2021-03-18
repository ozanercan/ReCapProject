using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class PaymentAddDto:IDto
    {
        public string RentId { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
    }
}
