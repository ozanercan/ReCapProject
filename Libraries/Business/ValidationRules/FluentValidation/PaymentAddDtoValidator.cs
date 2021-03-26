using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentAddDtoValidator : AbstractValidator<PaymentAddDto>
    {
        public PaymentAddDtoValidator()
        {
            RuleFor(p => p.RentalId).NotEmpty();
            RuleFor(p => p.RentalId.Length).GreaterThan(0);

            RuleFor(p => p.MoneyPaid).NotEmpty();
            RuleFor(p => p.MoneyPaid).GreaterThan(0);
        }
    }
}
