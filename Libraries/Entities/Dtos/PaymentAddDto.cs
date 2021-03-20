using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class PaymentAddDto:IDto
    {
        public string RentalId { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public decimal MoneyPaid { get; set; }
    }
}
