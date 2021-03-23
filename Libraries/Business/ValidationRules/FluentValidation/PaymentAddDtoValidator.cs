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

            RuleFor(p => p.NameSurname).NotEmpty();
            RuleFor(p => p.NameSurname).MaximumLength(100);
            RuleFor(p => p.NameSurname.Length).GreaterThan(3);

            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber).MaximumLength(50);
            RuleFor(p => p.CardNumber.Length).GreaterThan(10);

            RuleFor(p => p.ExpiryDate).NotEmpty();
            RuleFor(p => p.ExpiryDate).MaximumLength(10);

            RuleFor(p => p.Cvv).NotEmpty();
            RuleFor(p => p.Cvv).MaximumLength(3);
            RuleFor(p => p.Cvv.Length).GreaterThanOrEqualTo(3);

        }
    }
}
