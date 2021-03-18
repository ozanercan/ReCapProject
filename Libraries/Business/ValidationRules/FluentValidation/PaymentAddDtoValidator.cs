using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentAddDtoValidator : AbstractValidator<PaymentAddDto>
    {
        public PaymentAddDtoValidator()
        {
            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber.Length).GreaterThan(10);

            RuleFor(p => p.NameSurname).NotEmpty();
            RuleFor(p => p.NameSurname.Length).GreaterThan(3);

            RuleFor(p => p.Cvv).NotEmpty();
            RuleFor(p => p.Cvv.Length).GreaterThanOrEqualTo(3);

            RuleFor(p => p.ExpiryDate).NotEmpty();
        }
    }
}
