using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage(Messages.EmptyUserId);
            RuleFor(c => c.UserId).GreaterThan(0).WithMessage(Messages.InvalidUserId);
            RuleFor(c => c.CityId).NotEmpty().WithMessage(Messages.EmptyCityId);
            RuleFor(c => c.CityId).GreaterThan(0).WithMessage(Messages.InvalidCityId);
            RuleFor(c => c.Address).NotEmpty().WithMessage(Messages.EmptyAddress);
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage(Messages.EmptyCompanyName);
            RuleFor(c => c.CompanyName).MinimumLength(5).WithMessage(Messages.InvalidCompanyNameLength);
            RuleFor(c => c.MersisNo).NotEmpty().WithMessage(Messages.EmptyMersisNo);
        }
    }
}
