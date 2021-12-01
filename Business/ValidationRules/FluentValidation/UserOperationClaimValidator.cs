using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(u => u.OperationClaimId).NotEmpty().WithMessage(Messages.EmptyOperationClaimId);
            RuleFor(u => u.OperationClaimId).GreaterThan(0).WithMessage(Messages.InvalidOperationClaimId);
            RuleFor(u => u.UserId).NotEmpty().WithMessage(Messages.EmptyUserId);
            RuleFor(u => u.UserId).GreaterThan(0).WithMessage(Messages.InvalidUserId);
        }
    }
}
