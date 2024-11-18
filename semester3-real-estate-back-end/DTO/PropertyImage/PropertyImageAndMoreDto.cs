
using semester3_real_estate_back_end.DTO.Property;

namespace semester3_real_estate_back_end.DTO.PropertyImage;

public class PropertyImageAndMoreDto
{
    public string ChapterId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public int ChapterNumber { get; set; }
    public int VolumeNumber { get; set; }

    public int ViewCount { get; set; }


    public PropertyDto Property { get; set; }
}