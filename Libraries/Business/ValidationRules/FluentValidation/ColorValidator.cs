using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name.Length).GreaterThan(2);
        }
    }
}
