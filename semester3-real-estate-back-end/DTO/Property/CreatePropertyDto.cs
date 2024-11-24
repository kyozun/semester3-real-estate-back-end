using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.Property;

public class CreatePropertyDto
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string Address { get; set; }
    [Required] public double Price { get; set; }
    
    // [Required] public string Furniture { get; set; }
    [Required] public double Area { get; set; }
    [Required] public int Floor { get; set; }
    [Required] public int Bedroom { get; set; }
    [Required] public int Bathroom { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    [Required] public List<IFormFile> propertyImages { get; set; }
    [Required] public IFormFile coverImage { get; set; }

    // ID
    [Required] public Guid DirectionId { get; set; }
    [Required] public Guid CategoryId { get; set; }
    [Required] public Guid PropertyTypeId { get; set; }
    [Required] public int WardId { get; set; }
    [Required] public Guid JuridicalId { get; set; }
}