
namespace semester3_real_estate_back_end.DTO.Property;

public class PropertyDto
{
    public string PropertyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public string Address { get; set; }
    public double Price { get; set; }
    public string Furniture { get; set; }
    public double Area { get; set; }
    public int Floor { get; set; }
    public int Bedroom { get; set; }
    public int Bathroom { get; set; }
    public int ViewCount { get; set; }
}