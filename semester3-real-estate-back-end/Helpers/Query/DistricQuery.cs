using System.ComponentModel;

namespace semester3_real_estate_back_end.Helpers.Query;

public class DistrictQuery
{
    public int? DistrictId { get; set; }

    public int? ProvinceId { get; set; }

    public string? Name { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}