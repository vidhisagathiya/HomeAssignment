public class Activity
{
    public int ActivityId { get; set; }       // Primary key
    public int DestinationId { get; set; }    // Location or destination reference
    public int Duration { get; set; }         // Duration in hours (or relevant unit)
    public double Cost { get; set; }          // Cost of the activity
}
