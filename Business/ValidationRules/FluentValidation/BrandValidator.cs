using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).NotEmpty().WithMessage(Messages.EmptyBrandName);
            RuleFor(b => b.BrandName).MinimumLength(3).WithMessage(Messages.InvalidBrandNameLength);
        }
    }
}
