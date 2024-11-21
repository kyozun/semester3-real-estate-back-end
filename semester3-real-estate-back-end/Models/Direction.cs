namespace semester3_real_estate_back_end.Models;

public class Direction
{
    public string DirectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation Property
    public List<Property> Properties { get; set; }
}