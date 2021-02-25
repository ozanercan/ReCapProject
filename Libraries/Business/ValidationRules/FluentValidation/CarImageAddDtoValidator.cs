using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageAddDtoValidator : AbstractValidator<CarImageAddDto>
    {
        public CarImageAddDtoValidator()
        {
            RuleFor(p => p.CarId).NotEmpty();
        }
    }
}
