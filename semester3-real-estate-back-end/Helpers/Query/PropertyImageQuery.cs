using System.ComponentModel;

namespace semester3_real_estate_back_end.Helpers.Query;

public class PropertyImageQuery
{
    public Guid? PropertyImageId { get; set; }
    public Guid? PropertyId { get; set; }

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}