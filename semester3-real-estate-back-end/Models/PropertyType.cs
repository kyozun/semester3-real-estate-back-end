﻿namespace semester3_real_estate_back_end.Models;

public class PropertyType
{
    public string PropertyTypeId { get; set; }
    public string Name { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation Property
    public List<Property> Properties { get; set; }
}