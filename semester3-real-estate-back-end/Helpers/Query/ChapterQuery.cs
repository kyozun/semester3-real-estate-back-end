using System.ComponentModel;
using semester4.Helpers.Enums;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class ChapterQuery
{
    public string? Title { get; set; }
    public List<Guid>? ChapterId { get; set; }
    public List<Guid>? MangaId { get; set; }
    public List<Guid>? UserId { get; set; }
    public List<int>? VolumeNumber { get; set; }
    public List<int>? ChapterNumber { get; set; }

    public List<Guid>? ContentRatingIds { get; set; }

    [DefaultValue("1975-04-30")] public DateTime? CreatedAtSince { get; set; }

    [DefaultValue("1975-04-30")] public DateTime? UpdatedAtSince { get; set; }

    [DefaultValue("1975-04-30")] public DateTime? PublishAtSince { get; set; }

    public ChapterOrderBy? OrderBy { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<ChapterInclude>? Includes { get; set; }
}

public class ChapterOrderBy
{
    public OrderBy? CreatedAt { get; set; }
    public OrderBy? UpdatedAt { get; set; }
    public OrderBy? VolumeNumber { get; set; }
    public OrderBy? ChapterNumber { get; set; }
}