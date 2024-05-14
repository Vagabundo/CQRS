namespace CQRS.Domain.Vehicles;

public record Coin
{
    public string? Code { get; init; }

    public static readonly Coin Usd = new ("USD");
    public static readonly Coin Eur = new ("EUR");
    public static readonly Coin None = new ("");

    private Coin(string code) => Code = code;

    public static readonly IReadOnlyCollection<Coin> All = new []{ Usd, Eur};
    public static Coin FromCode(string code)
    { 
        return All.FirstOrDefault(x => x.Code == code) ?? throw new ApplicationException("Invalid coin");
    }
}