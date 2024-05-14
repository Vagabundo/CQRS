namespace CQRS.Domain.Vehicles;

public record Money(decimal Ammount, Coin Coin)
{
    public static Money operator +(Money first, Money second)
    {
        if(first.Coin != second.Coin)
        {
            throw new InvalidOperationException("Coins have to be equal");
        }

        return new Money(first.Ammount + second.Ammount, second.Coin);
    }

    public static Money Zero() => new(0, Coin.None);
    public static Money Zero(Coin coin) => new(0, coin);
    public bool IsZero() => this == Zero(Coin);
}
