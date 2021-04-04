using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName.Length).GreaterThan(2);
            RuleFor(p => p.FirstName).MaximumLength(50);

            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.LastName.Length).GreaterThan(2);
            RuleFor(p => p.LastName).MaximumLength(50);

            RuleFor(p => p.CompanyName).NotEmpty();
            RuleFor(p => p.CompanyName.Length).GreaterThan(2);
            RuleFor(p => p.CompanyName).MaximumLength(50);

            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.Email.Length).GreaterThan(2);
            RuleFor(p => p.Email).MaximumLength(50);

            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password.Length).GreaterThan(2);
            RuleFor(p => p.Password).MaximumLength(50);

            //RuleFor(p => p.Password).NotEmpty();
            //RuleFor(p => p.Password.Length).GreaterThan(3);
        }
    }
}
