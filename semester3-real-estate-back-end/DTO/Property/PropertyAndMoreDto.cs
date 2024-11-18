using semester3_real_estate_back_end.DTO.Category;
using semester3_real_estate_back_end.DTO.Juridical;
using semester3_real_estate_back_end.DTO.PropertyImage;
using semester3_real_estate_back_end.DTO.PropertyType;
using semester3_real_estate_back_end.DTO.User;
using semester3_real_estate_back_end.DTO.Ward;
using semester4.DTO.Chapter;

namespace semester3_real_estate_back_end.DTO.Property;

public class PropertyAndMoreDto
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


    public List<PropertyImageDto> PropertyImages { get; set; }
    public CategoryDto Category { get; set; }
    public DirectionDto Direction { get; set; }
    public WardDto Ward { get; set; }
    public JuridicalDto Juridical { get; set; }
    public PropertyTypeDto PropertyType { get; set; }
    public UserDto User { get; set; }
}