using Microsoft.EntityFrameworkCore;

public class TripService : ITripService
{
    private readonly AppDbContext _context;

    public TripService(AppDbContext context)
    {
        _context = context;
    }

    // Create a new trip from the provided DTO
    public async Task<Trip> CreateTripAsync(TripCreateDto dto)
    {
        try
        {
            // Validate that EndDate is not before StartDate
            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("EndDate must be greater than or equal to StartDate.");

            // Map DTO to Trip entity
            var trip = TripMapper.ToEntity(dto);

            // Save to DB
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return trip;
        }
        catch (Exception ex)
        {
            // Log the error and rethrow
            Console.WriteLine($"Error creating trip: {ex.Message}");
            throw;
        }
    }

    // Fetch a trip by ID, including its related activities
    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        return await _context.Trips
            .Include(t => t.Activities)
            .FirstOrDefaultAsync(t => t.TripId == id);
    }
}
