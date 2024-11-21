using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Enums.Include;

namespace semester3_real_estate_back_end.Helpers.Query;

public class PropertyQuery
{
    public List<Guid>? PropertyIds { get; set; }
    public List<Guid>? UserIds { get; set; }
    public List<Guid>? DirectionIds { get; set; }
    public List<Guid>? PropertyTypeIds { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public List<Guid>? JuridicalIds { get; set; }
    public int? WardId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CoverImage { get; set; }
    public string? Address { get; set; }

    [RegularExpression(@"^\d+-\d+$", ErrorMessage = "Invalid price range format")]
    public string? Price { get; set; }

    public string? Furniture { get; set; }
    
    [RegularExpression(@"^\d+-\d+$", ErrorMessage = "Invalid area range format")]
    public string? Area { get; set; }
    public int? Floor { get; set; }
    public int? Bedroom { get; set; }
    public int? Bathroom { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}