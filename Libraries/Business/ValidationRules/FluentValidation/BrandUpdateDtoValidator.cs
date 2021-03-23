using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandUpdateDtoValidator : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id).GreaterThan(0);

            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(50);
            RuleFor(p => p.Name.Length).GreaterThanOrEqualTo(2);
        }
    }
}
