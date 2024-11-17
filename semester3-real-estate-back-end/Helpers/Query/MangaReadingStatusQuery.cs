using System.ComponentModel;
using semester4.Helpers.Enums;

namespace semester4.Helpers.Query;

public class MangaReadingStatusQuery
{
    public Guid? MangaId { get; set; }
    public MangaReadingStatus? MangaReadingStatus { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}