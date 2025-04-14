public class Trip
{
    public int TripId { get; set; }               // Primary key
    public string UserId { get; set; }            // Owner of the trip
    public string Title { get; set; }             // Trip name or title
    public DateTime StartDate { get; set; }       // Trip start date
    public DateTime EndDate { get; set; }         // Trip end date
    public double TotalCost { get; set; }         // Total estimated cost
    public List<Activity> Activities { get; set; } // List of activities in this trip
}
