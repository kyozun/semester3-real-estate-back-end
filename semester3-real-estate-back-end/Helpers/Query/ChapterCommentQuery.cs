using System.ComponentModel;

namespace semester4.Helpers.Query;

public class ChapterCommentQuery
{
    public string? Name { get; set; } = null;
    public DateTime? CreatedAt { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
    public string? Include { get; set; } = null;
}