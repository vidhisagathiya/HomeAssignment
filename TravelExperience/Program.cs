using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Enable controller-based API endpoints
builder.Services.AddControllers();

// Use MS SQL and connect using config value (DefaultConnection from appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inject TripService wherever ITripService is used
builder.Services.AddScoped<ITripService, TripService>();

var app = builder.Build();

// Enforce HTTPS for all incoming requests
app.UseHttpsRedirection();

// Route incoming HTTP requests to corresponding controllers
app.MapControllers();

app.Run(); // Start the app
