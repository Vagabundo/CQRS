using FluentValidation;

namespace CQRS.Application.Rents.RentBooking;

public class RentBookingCommandValidator : AbstractValidator<RentBookingCommand>
{
    public RentBookingCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehicleId).NotEmpty();
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}