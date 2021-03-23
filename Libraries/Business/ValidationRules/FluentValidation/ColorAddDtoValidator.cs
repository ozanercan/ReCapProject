using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorAddDtoValidator : AbstractValidator<ColorAddDto>
    {
        public ColorAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name.Length).GreaterThanOrEqualTo(2);
            RuleFor(p => p.Name).MaximumLength(50);
        }
    }
}
