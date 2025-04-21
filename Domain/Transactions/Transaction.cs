using Domain.Categories;

namespace Domain.Transactions;

public sealed class Transaction
{
    public long Id { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }
    public long CategoryId { get; private set; }
    public Category Category { get; private set; }
    public long UserId { get; private set; }

    private Transaction() { } // For EF Core

    public Transaction(Money amount, string description,
        Category category, long userId)
    {
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Date = DateTime.Now.ToUniversalTime();
        Category = category ?? throw new ArgumentNullException(nameof(category));
        CategoryId = category.Id;
        UserId = userId;
    }

    public void UpdateAmount(Money newAmount)
    {
        Amount = newAmount ?? throw new ArgumentNullException(nameof(newAmount));
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription ?? throw new ArgumentNullException(nameof(newDescription));
    }

    public void UpdateCategory(Category newCategory)
    {
        Category = newCategory ?? throw new ArgumentNullException(nameof(newCategory));
        CategoryId = newCategory.Id;
    }
}