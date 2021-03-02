using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName.Length).GreaterThan(2);

            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.LastName.Length).GreaterThan(2);

            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Email).EmailAddress();

            //RuleFor(p => p.Password).NotEmpty();
            //RuleFor(p => p.Password.Length).GreaterThan(3);
        }
    }
}
