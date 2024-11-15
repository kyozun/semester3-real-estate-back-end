namespace semester3_real_estate_back_end.Models;

public class District
{
    public string DistrictId { get; set; }
    public string Name { get; set; }

    // FK
    public string ProvinceId { get; set; }

    //Navigation Property
    public Province Province { get; set; }
    public List<Ward> WardId { get; set; }
}