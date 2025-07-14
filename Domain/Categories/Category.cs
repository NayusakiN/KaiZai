using Domain.Transactions;
using SharedKernel;

namespace Domain.Categories;

public sealed class Category : Entity
{
    public Guid UserId { get; private set; }
    public Icon Icon { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CategoryType CategoryType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } // Add last update timestamp
    
    public IEnumerable<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public bool IsActive { get; private set; } = true; // Add active flag instead of deletion

    // For EF Core
    private Category() { }

    public Category(string name, Guid userId, Icon icon, CategoryType categoryType, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));
            
        //TODO: research this approach
        if (Enum.IsDefined(categoryType))
        {
            
        }
        Name = name;
        Icon = icon;
        UserId = userId;
        CategoryType = categoryType;
        CreatedAt = DateTime.UtcNow;
        Description = description;
    }
    
    public void UpdateIcon(Icon newIcon)
    {
        Icon = newIcon ?? throw new ArgumentNullException(nameof(newIcon), "Icon cannot be null");
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Category name cannot be empty", nameof(newName));
            
        Name = newName;
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