using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CarCreditScore:IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int MinCreditScore { get; set; }
    }
}
