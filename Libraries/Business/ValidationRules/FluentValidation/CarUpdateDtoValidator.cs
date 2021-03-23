using Business.Constants;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarUpdateDtoValidator : AbstractValidator<CarUpdateDto>
    {
        public CarUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id).GreaterThan(0);

            RuleFor(p => p.BrandName).NotEmpty();

            RuleFor(p => p.ColorName).NotEmpty();

            RuleFor(p => p.ModelYear).NotEmpty();

            RuleFor(p => p.DailyPrice).NotEmpty();
            RuleFor(p => p.DailyPrice).GreaterThan(0).WithMessage(Messages.CarDailyPriceInvalid);

            RuleFor(p => p.Description.Length).GreaterThan(2).WithMessage(Messages.CarDescriptionInvalid).When(p => !string.IsNullOrEmpty(p.Description));
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
