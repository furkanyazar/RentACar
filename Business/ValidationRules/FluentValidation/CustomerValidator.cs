using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage(Messages.EmptyUserId);
            RuleFor(c => c.UserId).GreaterThan(0).WithMessage(Messages.InvalidUserId);
            RuleFor(c => c.IDNo).NotEmpty().WithMessage(Messages.EmptyIDNo);
            RuleFor(c => c.IDNo).Length(11).WithMessage(Messages.InvalidIDNoLength);
            RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage(Messages.EmptyDateOfBirth);
        }
    }
}
