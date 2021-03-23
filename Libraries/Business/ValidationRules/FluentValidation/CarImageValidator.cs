using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(p => p.CarId).NotEmpty();
            RuleFor(p => p.CarId).GreaterThan(0);

            RuleFor(p => p.ImagePath).NotEmpty();
            RuleFor(p => p.ImagePath.Length).GreaterThan(15);
            RuleFor(p => p.ImagePath).MaximumLength(500);

            RuleFor(p => p.Date).NotNull();
            RuleFor(p => p.Date).NotEmpty();
        }
    }
}
