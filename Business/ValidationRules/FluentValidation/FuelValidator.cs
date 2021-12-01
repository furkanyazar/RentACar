using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FuelValidator : AbstractValidator<Fuel>
    {
        public FuelValidator()
        {
            RuleFor(f => f.FuelName).NotEmpty().WithMessage(Messages.EmptyFuelName);
            RuleFor(f => f.FuelName).MinimumLength(3).WithMessage(Messages.InvalidFuelNameLength);
        }
    }
}
