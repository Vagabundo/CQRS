using CQRS.Application.Abstractions.Clock;
using CQRS.Application.Abstractions.Messaging;
using CQRS.Domain.Abstractions;
using CQRS.Domain.Rents;
using CQRS.Domain.Users;
using CQRS.Domain.Vehicles;

namespace CQRS.Application.Rents.RentBooking;

internal sealed class RentBookingCommandHandler : ICommandHandler<RentBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentRepository _rentRepository;
    private readonly CostService _costService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RentBookingCommandHandler(IUserRepository userRepository, IVehicleRepository vehicleRepository,
        IRentRepository rentRepository, CostService costService, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _rentRepository = rentRepository;
        _costService = costService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(RentBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if(user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
        if(vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);
        if(await _rentRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
        {
            return Result.Failure<Guid>(RentErrors.Overlap);
        }

        var rent = Rent.Book(vehicle, user.Id, duration, _dateTimeProvider.CurrentTime, _costService);
        _rentRepository.Add(rent);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return rent.Id;
    }
}