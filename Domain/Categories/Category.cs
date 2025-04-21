namespace Domain.Categories;

public sealed class Category
{
    public long Id { get; private set; }
    public long UserId { get; private set; }
    public string ColorCode { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public CategoryType CategoryType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } // Add last update timestamp
    public bool IsActive { get; private set; } = true; // Add active flag instead of deletion

    // For EF Core
    private Category() { }

    public Category(string name, string colorCode, long userId, CategoryType categoryType)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));
            
        if (string.IsNullOrWhiteSpace(colorCode))
            throw new ArgumentException("Color code cannot be empty", nameof(colorCode));

        //TODO: research this approach
        if (Enum.IsDefined(categoryType))
        {
            
        }
        Name = name;
        ColorCode = colorCode;
        UserId = userId;
        CategoryType = categoryType;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Category name cannot be empty", nameof(newName));
            
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateColorCode(string newColorCode)
    {
        if (string.IsNullOrWhiteSpace(newColorCode))
            throw new ArgumentException("Color code cannot be empty", nameof(newColorCode));
            
        ColorCode = newColorCode;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateCategoryType(CategoryType newCategoryType)
    {
        CategoryType = newCategoryType;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}