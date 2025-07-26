using Application.Contracts.Messaging;
using Domain.Categories;

namespace Application.Categories.Create;

public sealed class CreateCategoryCommand() : ICommand<Guid>
{
    public Guid UserId { get; private set; }
    public Icon Icon { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CategoryType CategoryType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } // Add last update timestamp
}
