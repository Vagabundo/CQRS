using CQRS.Application.Rents.GetRent;
using CQRS.Application.Rents.RentBooking;
using CQRS.Domain.Rents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers.Rents;

[ApiController]
[Route("api/[controller]")]
public class RentsController : ControllerBase
{
    private readonly ISender _sender;

    public RentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRent(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRentQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> BookingRent(BookingRentRequest request, CancellationToken cancellationToken)
    {
        var command = new RentBookingCommand(request.VehicleId, request.UserId, request.StartDate, request.EndDate);
        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : CreatedAtAction(nameof(GetRent), new { id = result.Value});
    }
}
