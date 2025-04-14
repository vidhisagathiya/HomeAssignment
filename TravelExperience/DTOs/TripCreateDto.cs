using System.ComponentModel.DataAnnotations;

public class TripCreateDto
{
    [Required]
    public string UserId { get; set; } // ID of the user creating the trip

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } // Trip title with validation constraints

    [Required]
    public DateTime StartDate { get; set; } // Start date of the trip

    [Required]
    public DateTime EndDate { get; set; } // End date of the trip

    [MinLength(1, ErrorMessage = "At least one activity is required.")]
    public List<ActivityDto> Activities { get; set; } // At least one activity must be provided
}
