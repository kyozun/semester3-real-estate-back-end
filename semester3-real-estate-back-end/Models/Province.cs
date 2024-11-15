namespace semester3_real_estate_back_end.Models;

public class Province
{
    public string ProvinceId { get; set; }
    public string Name { get; set; }

    // Navigation Property
    public List<District> Districts { get; set; }
}