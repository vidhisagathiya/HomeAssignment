using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    // POST: api/trips
    // Creates a new trip
    [HttpPost]
    public async Task<IActionResult> CreateTrip([FromBody] TripCreateDto tripDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // Return 400 if validation fails

        try
        {
            var createdTrip = await _tripService.CreateTripAsync(tripDto);

            var readDto = TripMapper.ToReadDto(createdTrip);

            // Return 201 with location header
            return CreatedAtAction(nameof(GetTrip), new { id = readDto.ExperienceId }, readDto);
        }
        catch (Exception ex)
        {
            // Log and return generic 500 response
            Console.Error.WriteLine($"Critical error: {ex.Message}");

            return StatusCode(500, new
            {
                Message = "Something went wrong. Please try again later."
            });
        }
    }

    // GET: api/trips/{id}
    // Returns a trip by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrip(int id)
    {
        try
        {
            var trip = await _tripService.GetTripByIdAsync(id);
            if (trip == null) return NotFound(); // Return 404 if not found

            var readDto = TripMapper.ToReadDto(trip);

            return Ok(readDto);
        }
        catch (Exception ex)
        {
            // Log and return generic 500 response
            Console.Error.WriteLine($"Critical error: {ex.Message}");

            return StatusCode(500, new
            {
                Message = "Unable to retrieve trip. Please try again later."
            });
        }
    }
}
