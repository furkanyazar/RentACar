using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(o => o.OperationClaimName).NotEmpty().WithMessage(Messages.EmptyOperationClaimName);
            RuleFor(o => o.OperationClaimName).MinimumLength(3).WithMessage(Messages.InvalidOperationClaimNameLength);
        }
    }
}
