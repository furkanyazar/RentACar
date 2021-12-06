using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName).NotEmpty().WithMessage(Messages.EmptyCityName);
            RuleFor(c => c.CityName).MinimumLength(3).WithMessage(Messages.InvalidCityNameLength);
        }
    }
}
