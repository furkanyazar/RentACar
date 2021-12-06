using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ActivationValidator : AbstractValidator<Activation>
    {
        public ActivationValidator()
        {
            RuleFor(a => a.ActivationCode).NotEmpty().WithMessage(Messages.EmptyActivationCode);
            RuleFor(a => a.ActivationCode).Length(6).WithMessage(Messages.InvalidActivationCodeLength);
            RuleFor(a => a.UserId).NotEmpty().WithMessage(Messages.EmptyUserId);
            RuleFor(a => a.UserId).GreaterThan(0).WithMessage(Messages.InvalidUserId);
        }
    }
}
