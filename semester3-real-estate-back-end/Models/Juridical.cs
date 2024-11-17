namespace semester3_real_estate_back_end.Models;

public class Juridical
{
    public string JuridicalId { get; set; }
    public string Name { get; set; }

    // Navigation Property
    public List<Property> Properties { get; set; }
}