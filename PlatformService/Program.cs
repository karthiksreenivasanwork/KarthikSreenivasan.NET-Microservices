using PlatformService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
* Service Configuration
-----------------------

* Adding an In-Memory database.
*/
builder.Services.AddDbContext<AppDbContext>(opt =>
 opt.UseInMemoryDatabase("InMem"));

/*
* Map the interface and the concrete implementation of the same.
* Hence if the caller asks for the interface, we provide the concrete class.
*/
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/*
* Stage In-Memory database data for our platform service.
*/
PrepDb.PrepPopulation(app);

app.Run();


