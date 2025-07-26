using Application.Categories.Update;
using Domain.Categories;

namespace Application.DTOs;

internal sealed record UpdateCategoryDto(Icon Icon, string Name, CategoryType CategoryType, string? Description, DateTime? UpdatedAt)
{
    // Constructor to map from UpdateCategoryCommand (assuming command has matching fields)
    public UpdateCategoryDto(UpdateCategoryCommand command)
        : this(command.Icon, command.Name, command.CategoryType, command.Description, command.UpdatedAt)
    {
    }
}