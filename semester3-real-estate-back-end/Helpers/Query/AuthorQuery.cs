using System.ComponentModel;
using semester4.Helpers.Enums;
using semester4.Helpers.Enums.Include;

namespace semester4.Helpers.Query;

public class AuthorQuery
{
    public List<Guid>? AuthorId { get; set; }

    public string? Name { get; set; }
    public AuthorOrderBy? OrderBy { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public List<AuthorInclude>? Includes { get; set; }
}

public class AuthorOrderBy
{
    public OrderBy? Name { get; set; }
}