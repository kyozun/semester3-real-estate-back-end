using System.ComponentModel;
using semester4.Helpers.Enums;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class UserMangaQuery
{
    public List<Guid>? UserMangaId { get; set; }
    public List<Guid>? MangaId { get; set; }
    public List<Guid>? UserId { get; set; }
    public UserMangaOrderBy? OrderBy { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<UserMangaInclude>? Includes { get; set; }
}

public class UserMangaOrderBy
{
    public OrderBy? CreatedAt { get; set; }
    public OrderBy? UpdatedAt { get; set; }
    public OrderBy? Volume { get; set; }
}