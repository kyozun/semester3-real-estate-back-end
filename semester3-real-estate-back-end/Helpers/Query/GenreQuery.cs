using System.ComponentModel;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class GenreQuery
{
    public List<Guid>? GenreId { get; set; }

    public string? Name { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }

    public List<GenreInclude>? Includes { get; set; }
}