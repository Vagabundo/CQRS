namespace CQRS.Domain.Rents;

public sealed record DateRange
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }

    private DateRange()
    {}

    public int Days => End.DayNumber - Start.DayNumber;

    public static DateRange Create (DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new ApplicationException("The end date is prior to start date");
        }

        return new DateRange
        {
            Start = start,
            End = end
        };
    }
}
