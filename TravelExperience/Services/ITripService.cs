// Service interface for managing trips
public interface ITripService
{
    // Create a new trip from a DTO
    Task<Trip> CreateTripAsync(TripCreateDto dto);

    // Get a trip by ID, including related data
    Task<Trip?> GetTripByIdAsync(int id);
}
