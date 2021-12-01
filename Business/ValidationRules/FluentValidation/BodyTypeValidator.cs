using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BodyTypeValidator : AbstractValidator<BodyType>
    {
        public BodyTypeValidator()
        {
            RuleFor(b => b.BodyTypeName).NotEmpty().WithMessage(Messages.EmptyBodyTypeName);
            RuleFor(b => b.BodyTypeName).MinimumLength(3).WithMessage(Messages.InvalidBodyTypeNameLength);
        }
    }
}
