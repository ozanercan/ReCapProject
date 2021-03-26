using Business.Constants;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarAddDtoValidator : AbstractValidator<CarAddDto>
    {
        public CarAddDtoValidator()
        {
            RuleFor(p => p.MinCreditScore).NotEmpty();
            RuleFor(p => p.MinCreditScore).LessThanOrEqualTo(1900);
            RuleFor(p => p.MinCreditScore).GreaterThanOrEqualTo(0);

            RuleFor(p => p.BrandName).NotEmpty();
            RuleFor(p => p.ColorName).NotEmpty();
            RuleFor(p => p.ModelYear).NotEmpty();
            RuleFor(p => p.DailyPrice).NotEmpty();
            RuleFor(p => p.DailyPrice).GreaterThan(0).WithMessage(Messages.CarDailyPriceInvalid);
            RuleFor(p => p.Description.Length).GreaterThan(2).WithMessage(Messages.CarDescriptionInvalid).When(p=>!string.IsNullOrEmpty(p.Description));
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
