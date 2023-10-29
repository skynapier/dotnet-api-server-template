using DotNetEnv;
using Microsoft.Extensions.Logging;

// Load environment variables from .env file
DotNetEnv.Env.Load();


var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddScoped<IClientService, ClientService>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Get the logger
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Log the ASPNETCORE_ENVIRONMENT value
logger.LogInformation($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

var useHttps = Environment.GetEnvironmentVariable("USE_HTTPS")?.ToLower() == "true";

if (useHttps)
{
    app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
