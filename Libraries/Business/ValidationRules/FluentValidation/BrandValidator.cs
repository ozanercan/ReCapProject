using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name.Length).GreaterThan(2);
        }
    }
}
