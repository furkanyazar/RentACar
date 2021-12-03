using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty().WithMessage(Messages.EmptyCarId);
            RuleFor(r => r.CarId).GreaterThan(0).WithMessage(Messages.InvalidCarId);
            RuleFor(r => r.CustomerId).NotEmpty().WithMessage(Messages.EmptyCustomerId);
            RuleFor(r => r.CustomerId).GreaterThan(0).WithMessage(Messages.InvalidCustomerId);
            RuleFor(r => r.RentDate).NotEmpty().WithMessage(Messages.EmptyRentDate);
            RuleFor(r => r.RentDate).GreaterThan(DateTime.Now).WithMessage(Messages.InvalidRentDate);
            RuleFor(r => r.ReturnDate).NotEmpty().WithMessage(Messages.EmptyReturnDate);
            RuleFor(r => r.ReturnDate).GreaterThan(r => r.RentDate).WithMessage(Messages.InvalidReturnDate);
        }
    }
}
