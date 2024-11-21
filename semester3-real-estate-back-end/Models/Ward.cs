namespace semester3_real_estate_back_end.Models;

public class Ward
{
    public int WardId { get; set; }
    public string Name { get; set; }            
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // FK
    public int DistrictId { get; set; }

    // Navigation Property
    public District District { get; set; }
    public List<Property> Properties { get; set; }
}