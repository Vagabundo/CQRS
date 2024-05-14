using CQRS.Domain.Vehicles;

namespace CQRS.Domain.Rents;

public record CostDetail
(
    Money CostPerPeriod,
    Money Maintenance,
    Money Accessories,
    Money TotalCost
);