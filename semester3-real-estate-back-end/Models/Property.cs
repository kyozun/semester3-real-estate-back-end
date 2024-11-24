namespace semester3_real_estate_back_end.Models;

public class Property
{
    public string PropertyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; }
    public double Price { get; set; }
    public double Area { get; set; }
    public int Floor { get; set; }
    public int Bedroom { get; set; }
    public int Bathroom { get; set; }
    public int ViewCount { get; set; } = 1;
    public string CoverImage { get; set; }

    public bool IsDraft { get; set; } = false;
    
    public DateTime ExpiryDate {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // FK
    public string DirectionId { get; set; }
    public string CategoryId { get; set; }
    public string PropertyTypeId { get; set; }
    public string JuridicalId { get; set; }
    public string UserId { get; set; }
    public int WardId { get; set; }

    // Navigation Property
    public List<PropertyImage> PropertyImages { get; set; }

    public Ward Ward { get; set; }
    public Juridical Juridical { get; set; }
    public Category Category { get; set; }
    public PropertyType PropertyType { get; set; }
    public Direction Direction { get; set; }
    public User User { get; set; }
}