using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester3_real_estate_back_end.Helpers.Query;

public class ProvinceQuery
{
    public Guid? ProvinceId { get; set; }

    public string? Name { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}