using Application.Contracts.Messaging;
using Application.Core;
using Domain.Categories;
using SharedKernel;

namespace Application.Categories.Update;

public sealed class UpdateCategoryCommand() : ICommand<Unit>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Icon Icon { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CategoryType CategoryType { get; set; }
    public DateTime? UpdatedAt { get; set; } // Add last update timestamp
}
