using CQRS.Application.Vehicles.SearchVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers.Vehicles;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly ISender _sender;

    public VehiclesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehicles(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var query = new SearchVehiclesQuery(startDate, endDate);
        var results = await _sender.Send(query, cancellationToken);

        return Ok(results.Value);
    }
}
