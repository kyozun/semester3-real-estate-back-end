namespace semester3_real_estate_back_end.Models;

public class District
{
    public int DistrictId { get; set; }
    public string Name { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // FK
    public int ProvinceId { get; set; }

    //Navigation Property
    public Province Province { get; set; }
    public List<Ward> Wards { get; set; }
}