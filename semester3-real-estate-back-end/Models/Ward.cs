namespace semester3_real_estate_back_end.Models;

public class Ward
{
    public string WardId { get; set; }
    public string Name { get; set; }

    // FK
    public string DistrictId { get; set; }

    // Navigation Property
    public District District { get; set; }
    public List<Property> Properties { get; set; }
}