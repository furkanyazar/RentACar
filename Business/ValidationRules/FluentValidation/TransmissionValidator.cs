using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TransmissionValidator : AbstractValidator<Transmission>
    {
        public TransmissionValidator()
        {
            RuleFor(t => t.TransmissionName).NotEmpty().WithMessage(Messages.EmptyTransmissionName);
            RuleFor(t => t.TransmissionName).MinimumLength(3).WithMessage(Messages.InvalidTransmissionNameLength);
        }
    }
}
