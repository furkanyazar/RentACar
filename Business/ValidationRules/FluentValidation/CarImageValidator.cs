using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(c => c.CarId).NotEmpty().WithMessage(Messages.EmptyCarId);
            RuleFor(c => c.CarId).GreaterThan(0).WithMessage(Messages.InvalidCarId);
            RuleFor(c => c.ImagePath).NotEmpty().WithMessage(Messages.EmptyImagePath);
        }
    }
}
