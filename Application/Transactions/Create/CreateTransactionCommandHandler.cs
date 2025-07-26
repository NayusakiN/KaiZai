using Application.Contracts.Authentication;
using Application.Contracts.Data;
using Application.Contracts.Messaging;
using Domain.Transactions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Transactions.Create;

public class CreateTransactionCommandHandler(
    IAppDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext
    ) : ICommandHandler<CreateTransactionCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateTransactionCommand command, CancellationToken cancellationToken = default)
    {
        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }
        
        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        var transaction = new Transaction(money: command.Amount,
            description: command.Description,
            category: command.Category,
            userId: command.UserId);
        
        context.Transactions.Add(transaction);
        
        await context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}