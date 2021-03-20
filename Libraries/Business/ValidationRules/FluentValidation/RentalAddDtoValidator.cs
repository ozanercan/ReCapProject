using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalAddDtoValidator : AbstractValidator<RentalAddDto>
    {
        public RentalAddDtoValidator()
        {
            RuleFor(p => p.CarId).NotEmpty();
            RuleFor(p => p.CustomerId).NotEmpty();
            RuleFor(p => p.RentDate).NotEmpty();
            RuleFor(p => p.ReturnDate).NotEmpty();
        }
    }
}
