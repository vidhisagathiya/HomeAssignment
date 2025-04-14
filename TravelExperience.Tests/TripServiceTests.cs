using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class TripServiceTests
{
    [Fact]
    public async Task CreateTripAsync_Should_CalculateTotalCostCorrectly()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "travelexperience")
            .Options;

        var context = new AppDbContext(options);
        var service = new TripService(context);

        var dto = new TripCreateDto
        {
            Title = "Test Trip",
            UserId = "user123",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            Activities = new List<ActivityDto>
            {
                new ActivityDto { Cost = 100, Duration = 60, DestinationId = 1 },
                new ActivityDto { Cost = 200, Duration = 120, DestinationId = 2 }
            }
        };

        // Act
        var result = await service.CreateTripAsync(dto);

        // Assert
        Assert.Equal(300, result.TotalCost);
        Assert.Equal(2, result.Activities.Count);
    }
}
