namespace semester3_real_estate_back_end.Models;

public class CustomList
{
    public string CustomListId { get; set; }

    // FK
    public string UserId { get; set; }
    public string PropertyId { get; set; }
}