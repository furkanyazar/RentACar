using Business.Constants;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Messages.EmptyEmailAddress);
            RuleFor(u => u.Email).EmailAddress().WithMessage(Messages.InvalidEmailAddress);
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage(Messages.EmptyPhoneNumber);
            RuleFor(u => u.PhoneNumber).Must(PhoneNumber).WithMessage(Messages.InvalidPhoneNumber);
            RuleFor(u => u.PhoneNumber).Length(11).WithMessage(Messages.InvalidPhoneNumberLength);
            RuleFor(u => u.Password).MinimumLength(8).WithMessage(Messages.InvalidPasswordLength);
            RuleFor(u => u.FirstName).MinimumLength(2).WithMessage(Messages.InvalidFirstNameLength);
            RuleFor(u => u.LastName).MinimumLength(2).WithMessage(Messages.InvalidLastNameLength);
        }

        private bool PhoneNumber(string arg)
        {
            return arg.StartsWith("0");
        }
    }
}
