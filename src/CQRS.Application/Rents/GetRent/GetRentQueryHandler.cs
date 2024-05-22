using CQRS.Application.Abstractions.Data;
using CQRS.Application.Abstractions.Messaging;
using CQRS.Domain.Abstractions;
using Dapper;

namespace CQRS.Application.Rents.GetRent;

internal sealed class GetRentQueryHandler : IQueryHandler<GetRentQuery, RentResponse>
{
    private readonly ISqlConnectionFactory _sqConnectionFactory;

    public GetRentQueryHandler(ISqlConnectionFactory sqConnectionFactory)
    {
        _sqConnectionFactory = sqConnectionFactory;
    }

    public async Task<Result<RentResponse>> Handle(GetRentQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqConnectionFactory.CreateConnection();

        var sql = """
            SELECT
                id AS Id,
                vehicle_id AS VehicleId,
                user_id AS UserId,
                status AS Status,
                cost_per_period AS RentCost,
                cost_per_period_coin AS RentCostCoin,
                maintenance_cost AS MaintenanceCost,
                maintenance_cost_coin AS MaintenanceCostCoin,
                accessories_cost AS AccessoriesCost,
                accessories_cost_coin AS AccessoriesCostCoin,
                total_cost AS TotalCost,
                total_cost_coin AS TotalCostCoin,
                duration_start AS DurationStart,
                duration_end AS DurationEnd,
                created_at AS CreatedAt
            FROM rents
            WHERE id=@RentId
        """;

        var rent = await connection.QueryFirstOrDefaultAsync<RentResponse>(
            sql,
            new {
                request.RentId
            }
        );

        return rent!;
    }
}