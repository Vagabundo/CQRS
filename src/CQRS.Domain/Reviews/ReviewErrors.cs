using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotElegible = new("Review.NotElegible", "This review is not elegible because the rent is not completed yet");
}