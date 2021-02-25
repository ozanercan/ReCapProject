using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.CompanyName).NotEmpty();
            RuleFor(p => p.CompanyName.Length).GreaterThan(2);
        }
    }
}
