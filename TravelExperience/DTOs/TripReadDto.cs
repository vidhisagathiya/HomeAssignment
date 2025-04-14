public class TripReadDto
{
    public string ExperienceId { get; set; }        // Trip ID (stringified for client use)
    public string Title { get; set; }               // Trip title
    public string UserId { get; set; }              // ID of the user who owns the trip
    public DateTime StartDate { get; set; }         // Trip start date
    public DateTime EndDate { get; set; }           // Trip end date
    public double TotalCost { get; set; }           // Total cost of the trip
    public List<ActivityDto> Activities { get; set; } // List of activities in this trip
}
