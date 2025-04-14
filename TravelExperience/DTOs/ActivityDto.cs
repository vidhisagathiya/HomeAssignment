using System.ComponentModel.DataAnnotations;

public class ActivityDto
{
    [Range(1, int.MaxValue, ErrorMessage = "DestinationId must be a positive number.")]
    public int DestinationId { get; set; } // Location reference (must be positive)

    [Range(1, 30, ErrorMessage = "Duration must be between 1 and 30 days.")]
    public int Duration { get; set; } // Duration in days (1–30 allowed)

    [Range(0, double.MaxValue, ErrorMessage = "Cost must be a non-negative value.")]
    public double Cost { get; set; } // Cost of the activity (can be 0 or more)
}
