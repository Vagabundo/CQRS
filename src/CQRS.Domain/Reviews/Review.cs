using CQRS.Domain.Abstractions;
using CQRS.Domain.Rents;
using CQRS.Domain.Reviews.Events;

namespace CQRS.Domain.Reviews;

public sealed class Review : Entity
{
    public Guid VehicleId { get; private set; }
    public Guid RentId { get; private set; }
    public Guid UserId { get; private set; }
    public Rating? Rating { get; private set; }
    public Comment? Comment { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    private Review(){}
    public Review(Guid id, Guid vehicleId, Guid rentId, Guid userId, Rating rating, Comment comment, DateTime? createdAt) : base(id)
    {
        VehicleId = vehicleId;
        RentId = rentId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedAt = createdAt;
    }

    public static Result<Review> Create(Rent rent, Rating rating, Comment comment, DateTime? createdAt)
    {
        if(rent.Status != RentStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotElegible);
        }

        var review = new Review(Guid.NewGuid(), rent.VehicleId, rent.Id, rent.UserId, rating, comment, createdAt);

        review.RaiseDomainEvent(new ReviewCreatedEvent(review.Id));

        return Result.Success(review);
    }
}