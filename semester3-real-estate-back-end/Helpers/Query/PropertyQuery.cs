using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester3_real_estate_back_end.Helpers.Query;

public class PropertyQuery
{
    public Guid? PropertyId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? DirectionId { get; set; }
    public Guid? PropertyTypeId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? WardId { get; set; }
    public Guid? JuridicalId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CoverImage { get; set; }
    public string? Address { get; set; }
    public string? Price { get; set; }
    public string? Furniture { get; set; }
    public decimal? Area { get; set; }
    public int? Floors { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}