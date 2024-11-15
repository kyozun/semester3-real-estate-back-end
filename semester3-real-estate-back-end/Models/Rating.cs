namespace semester3_real_estate_back_end.Models;

public class Rating
{
    public string RatingId { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Foreign Key


    // Navigation property
}