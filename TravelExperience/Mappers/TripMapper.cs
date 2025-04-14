public static class TripMapper
{
    // Convert TripCreateDto to Trip entity for DB save
    public static Trip ToEntity(TripCreateDto dto)
    {
        return new Trip
        {
            Title = dto.Title,
            UserId = dto.UserId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            // Map each ActivityDto to Activity entity
            Activities = dto.Activities.Select(a => new Activity
            {
                Cost = a.Cost,
                Duration = a.Duration,
                DestinationId = a.DestinationId
            }).ToList(),
            // Calculate total cost from all activities
            TotalCost = dto.Activities.Sum(a => a.Cost)
        };
    }

    // Convert Trip entity to TripReadDto for client response
    public static TripReadDto ToReadDto(Trip trip)
    {
        return new TripReadDto
        {
            ExperienceId = trip.TripId.ToString(),
            Title = trip.Title,
            UserId = trip.UserId,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            TotalCost = trip.TotalCost,
            // Map each Activity to ActivityDto
            Activities = trip.Activities.Select(a => new ActivityDto
            {
                Cost = a.Cost,
                Duration = a.Duration,
                DestinationId = a.DestinationId
            }).ToList()
        };
    }
}
