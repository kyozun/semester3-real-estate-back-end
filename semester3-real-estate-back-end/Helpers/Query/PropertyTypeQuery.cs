using System.ComponentModel;

namespace semester3_real_estate_back_end.Helpers.Query;

public class PropertyTypeQuery
{
    public Guid? PropertyTypeId { get; set; }
    public string? Name { get; set; }
    
    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}