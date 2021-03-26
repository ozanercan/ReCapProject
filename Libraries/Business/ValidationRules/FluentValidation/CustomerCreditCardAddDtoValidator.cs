using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerCreditCardAddDtoValidator : AbstractValidator<CustomerCreditCard>
    {
        public CustomerCreditCardAddDtoValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.UserId).GreaterThan(0);

            RuleFor(p => p.CardOwnerFullName).NotEmpty();
            RuleFor(p => p.CardOwnerFullName).MaximumLength(50);
            RuleFor(p => p.CardOwnerFullName.Length).GreaterThan(3);

            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber).MaximumLength(20);
            RuleFor(p => p.CardNumber.Length).GreaterThan(10);

            RuleFor(p => p.ExpiryDate).NotEmpty();
            RuleFor(p => p.ExpiryDate).MaximumLength(5);

            RuleFor(p => p.Cvv).NotEmpty();
            RuleFor(p => p.Cvv).MaximumLength(3);
        }
    }
}
