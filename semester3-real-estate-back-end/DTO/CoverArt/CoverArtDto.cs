namespace semester4.DTO.CoverArt;

public class CoverArtDto
{
    public string CoverArtId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Locale { get; set; } = string.Empty;

    public string Volume { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}