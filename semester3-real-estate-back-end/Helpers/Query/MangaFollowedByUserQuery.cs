using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class MangaFollowedByUserQuery
{
    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<MangaInclude>? Includes { get; set; }
}