namespace Domain.Categories;

public sealed class Icon
{
    public string Key { get; private set; }
    public string ColorCode { get; private set; }

    private Icon() { } // For EF Core

    public Icon(string key, string colorCode)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Icon key cannot be empty", nameof(key));
            
        if (string.IsNullOrWhiteSpace(colorCode))
            throw new ArgumentException("Color code cannot be empty", nameof(colorCode));
            
        Key = key;
        ColorCode = colorCode;
    }

    // Factory method for creating an icon with validation
    public static Icon Create(string key, string colorCode)
    {
        return new Icon(key, colorCode);
    }
}