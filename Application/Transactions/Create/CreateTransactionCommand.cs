using Application.Contracts.Messaging;
using Domain.Categories;
using Domain.Transactions;
using SharedKernel;

namespace Application.Transactions.Create;

public sealed class CreateTransactionCommand : ICommand<Guid>
{
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Domain.Categories.Category Category { get; private set; }
    public Guid UserId { get; private set; }
}