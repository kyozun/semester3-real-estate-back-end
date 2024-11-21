using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester3_real_estate_back_end.Helpers.Query;

public class WardQuery
{
    public int? WardId { get; set; }

    public int? DistrictId { get; set; }
    public string? Name { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}