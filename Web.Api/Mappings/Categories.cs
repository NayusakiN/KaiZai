using Application.Categories.Update;
using Web.Api.Endpoints.Categories;

namespace Web.Api.Mappings;

internal static class Categories
{
    public static UpdateCategoryCommand ToCommand(this Update.Request request) =>
        new()
        {
            Id = request.Id,
            UserId = request.UserId,
            Icon = request.Icon,
            Name = request.Name,
            Description = request.Description,
            CategoryType = request.CategoryType,
            UpdatedAt = DateTime.UtcNow // Set the timestamp when mapping
        };
}