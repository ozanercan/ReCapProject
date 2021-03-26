using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class PaymentAddDto : IDto
    {
        public string RentalId { get; set; }
        public decimal MoneyPaid { get; set; }
    }
}
