using Application.Categories.Create;
using Application.Contracts.Authentication;
using Application.Contracts.Data;
using Application.Contracts.Messaging;
using Application.Core;
using Application.DTOs;
using Domain.Categories;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.Update;

internal sealed class UpdateCategoryCommandHandler(
    IAppDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Unit>(UserErrors.Unauthorized());
        }

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Unit>(UserErrors.NotFound(command.UserId));
        }
        
        var existingCategory = await context.Categories
            .SingleOrDefaultAsync(c => c.Id == command.Id && c.UserId == command.UserId, cancellationToken);
        
        if (existingCategory is null)
        {
            return Result.Failure<Unit>(Error.NotFound("", ""));
        }
        
        var updateCategoryDto = new UpdateCategoryDto(command);

        context.Categories.Entry(existingCategory).CurrentValues.SetValues(updateCategoryDto);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}