using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class RatingQuery
{
    public List<Guid>? RatingId { get; set; }
    public List<Guid>? MangaId { get; set; }

    public List<Guid>? UserId { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<RatingInclude>? Includes { get; set; }
}