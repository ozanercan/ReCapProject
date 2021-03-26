using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarCreditScoreAddDtoValidator : AbstractValidator<CarCreditScoreAddDto>
    {
        public CarCreditScoreAddDtoValidator()
        {
            RuleFor(p => p.CarId).NotNull();
            RuleFor(p => p.CarId).NotEmpty();
            RuleFor(p => p.CarId).GreaterThan(0);

            RuleFor(p => p.MinCreditScore).NotNull();
            RuleFor(p => p.MinCreditScore).NotEmpty();
            RuleFor(p => p.MinCreditScore).GreaterThanOrEqualTo(0);
            RuleFor(p => p.MinCreditScore).LessThanOrEqualTo(1900);
        }
    }
}
