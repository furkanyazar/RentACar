using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TractionValidator : AbstractValidator<Traction>
    {
        public TractionValidator()
        {
            RuleFor(t => t.TractionName).NotEmpty().WithMessage(Messages.EmptyTractionName);
            RuleFor(t => t.TractionName).MinimumLength(3).WithMessage(Messages.InvalidTractionNameLength);
        }
    }
}
