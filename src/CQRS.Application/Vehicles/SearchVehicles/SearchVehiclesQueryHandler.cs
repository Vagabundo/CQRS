using CQRS.Application.Abstractions.Data;
using CQRS.Application.Abstractions.Messaging;
using CQRS.Domain.Abstractions;
using CQRS.Domain.Rents;
using Dapper;

namespace CQRS.Application.Vehicles.SearchVehicles;

internal sealed class SearchVehiclesQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
{
    private static readonly int[] ActiveRentStatuses = { (int)RentStatus.Booked, (int)RentStatus.Confirmed, (int)RentStatus.Completed};
    private readonly ISqlConnectionFactory _sqConnectionFactory;

    public SearchVehiclesQueryHandler(ISqlConnectionFactory sqConnectionFactory)
    {
        _sqConnectionFactory = sqConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        if(request.StartDate > request.EndDate)
        {
            return new List<VehicleResponse>();
        }

        using var connection = _sqConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.id as Id,
                a.model AS Model,
                a.vin AS Vin,
                a.cost_ammount AS Cost,
                a.cost_coin AS Coin,
                a.address_street AS Street,
                a.address_number AS Number,
                a.address_city AS City,
                a.address_province AS Province,
                a.address_country AS Country
            FROM vehicles AS a
            WHERE NOT EXISTS
            (
                    SELECT 1
                    FROM rents AS b
                    WHERE
                        b.vehicle_id = a.id AND
                        b.duration_start <= @EndDate AND
                        b.duration_end >= @StartDate AND
                        b.status = ANY(@ActiveRentStatuses)
            )
        """;

        var vehicles = await connection.QueryAsync<VehicleResponse, AddressResponse, VehicleResponse>(
            sql,
            (vehicle, address) => {
                vehicle.Address = address;
                return vehicle;
            },
            new { StartDate = request.StartDate, EndDate = request.EndDate, ActiveRentStatuses },
            splitOn: "Street"
        );

        return vehicles.ToList();
    }
}