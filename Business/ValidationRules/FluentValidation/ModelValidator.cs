using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(m => m.ModelName).NotEmpty().WithMessage(Messages.EmptyModelName);
            RuleFor(m => m.ModelName).MinimumLength(2).WithMessage(Messages.InvalidModelNameLength);
            RuleFor(m => m.BrandId).NotEmpty().WithMessage(Messages.EmptyBrandId);
            RuleFor(m => m.BrandId).GreaterThan(0).WithMessage(Messages.InvalidBrandId);
        }
    }
}
