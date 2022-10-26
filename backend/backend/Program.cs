using Ifuel.Models;
using Ifuel.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<FuelStationDatabaseSettings>(builder.Configuration.GetSection("FuelStationCollection"));
builder.Services.Configure<FuelQueueDatabaseSettings>(builder.Configuration.GetSection("FuelQueueCollection"));
builder.Services.AddSingleton<FuelStationService>();
builder.Services.AddSingleton<FuelQueueService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddScoped<IService, FuelStationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
