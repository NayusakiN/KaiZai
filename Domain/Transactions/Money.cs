namespace Domain.Transactions;

//TODO: add feature for base currency and conversion
public sealed class Money
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    //TODO: remove later maybe?
    private Money() { } // For EF Core

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    //TODO: remove later maybe, depends on feature of arithmetic operations
    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException("Cannot add amounts with different currencies.");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException("Cannot subtract amounts with different currencies.");

        return new Money(Amount - other.Amount, Currency);
    }
}