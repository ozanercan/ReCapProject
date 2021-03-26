using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public decimal MoneyPaid { get; set; }
    }
}
