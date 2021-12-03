using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BodyTypeId).NotEmpty().WithMessage(Messages.EmptyBodyTypeId);
            RuleFor(c => c.BodyTypeId).GreaterThan(0).WithMessage(Messages.InvalidBodyType);
            RuleFor(c => c.CompanyId).NotEmpty().WithMessage(Messages.EmptyCompanyId);
            RuleFor(c => c.CompanyId).GreaterThan(0).WithMessage(Messages.InvalidCompany);
            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage(Messages.EmptyDailyPrice);
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage(Messages.InvalidDailyPrice);
            RuleFor(c => c.EngineSize).NotEmpty().WithMessage(Messages.EmptyEngineSize);
            RuleFor(c => c.EngineSize).Must(GreaterThan).WithMessage(Messages.InvalidEngineSize);
            RuleFor(c => c.FuelId).NotEmpty().WithMessage(Messages.EmptyFuelId);
            RuleFor(c => c.FuelId).GreaterThan(0).WithMessage(Messages.InvalidFuel);
            RuleFor(c => c.ModelId).NotEmpty().WithMessage(Messages.EmptyModelId);
            RuleFor(c => c.ModelId).GreaterThan(0).WithMessage(Messages.InvalidModel);
            RuleFor(c => c.ModelYear).NotEmpty().WithMessage(Messages.EmptyModelYear);
            RuleFor(c => c.ModelYear).Must(GreaterThan).WithMessage(Messages.InvalidModelYear);
            RuleFor(c => c.Seats).NotEmpty().WithMessage(Messages.EmptySeats);
            RuleFor(c => c.Seats).Must(GreaterThan).WithMessage(Messages.InvalidSeats);
            RuleFor(c => c.TractionId).NotEmpty().WithMessage(Messages.EmptyTractionId);
            RuleFor(c => c.TractionId).GreaterThan(0).WithMessage(Messages.InvalidTraction);
            RuleFor(c => c.TransmissionId).NotEmpty().WithMessage(Messages.EmptyTransmissionId);
            RuleFor(c => c.TransmissionId).GreaterThan(0).WithMessage(Messages.InvalidTransmission);
        }

        private bool GreaterThan(short arg)
        {
            return arg > 0;
        }
    }
}
