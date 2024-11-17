using System.ComponentModel;
using semester4.Helpers.Enums;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class MangaQuery
{
    public List<Guid>? MangaIds { get; set; }
    public List<Guid>? AuthorIds { get; set; }
    public List<Guid>? GenreIds { get; set; }
    public List<Guid>? ContentRatingIds { get; set; }

    public string? Title { get; set; }
    public int? Year { get; set; }
    public List<MangaStatus>? Status { get; set; }


    [DefaultValue("1975-04-30")] public DateTime? CreatedAtSince { get; set; }

    [DefaultValue("1975-04-30")] public DateTime? UpdatedAtSince { get; set; }

    [DefaultValue("1975-04-30")] public DateTime? PublishAtSince { get; set; }
    public MangaOrderBy? OrderBy { get; set; }
    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<MangaInclude>? Includes { get; set; }
}

public class MangaOrderBy
{
    public OrderBy? Title { get; set; }
}