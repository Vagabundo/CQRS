using CQRS.Application.Abstractions.Email;
using CQRS.Domain.Rents;
using CQRS.Domain.Rents.Events;
using CQRS.Domain.Users;
using MediatR;

namespace CQRS.Application.Rents.RentBooking;

internal sealed class RentBookingEventHandler : INotificationHandler<RentBookingEvent>
{
    private readonly IRentRepository _rentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public RentBookingEventHandler(IRentRepository rentRepository, IUserRepository userRepository, IEmailService emailService)
    {
        _rentRepository = rentRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(RentBookingEvent notification, CancellationToken cancellationToken)
    {
        var rent = await _rentRepository.GetByIdAsync(notification.rentId, cancellationToken);
        if(rent is null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(rent.UserId, cancellationToken);
        if(user is null)
        {
            return;
        }

        await _emailService.SendAsync(user.Email!, "Rent booked", "This booking has to be confirmed, otherwise it will be reverted");
    }
}