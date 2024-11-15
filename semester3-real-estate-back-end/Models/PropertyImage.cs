namespace semester3_real_estate_back_end.Models;

public class PropertyImage
{
    public string PropertyImageId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }

    // FK
    public string PropertyId { get; set; }

    // Navigation Property
    public Property Property { get; set; }
}