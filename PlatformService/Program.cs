using PlatformService.Data;
using Microsoft.EntityFrameworkCore;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _config = builder.Configuration;
IWebHostEnvironment _env = builder.Environment;


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"Database connection string used: {_config.GetConnectionString("PlatformsConn")}");

if (_env.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(_config.GetConnectionString("PlatformsConn"))
    );
}
else
{
    Console.WriteLine("--> Using InMem Db");
    /*
    * Service Configuration
    -----------------------
    * Adding an In-Memory database.
    */
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}

/*
* Map the interface and the concrete implementation of the same.
* Hence if the caller asks for the interface, we provide the concrete class.
*/
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
/*
* HTTP Client Factory
* Hence, when we ask for ICommandDataClient, we will get HttpCOmmandDataClient.
* This service registration is to support dependency injection.
* Example: We are injecting this in PlatformsController.
*/
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

/*
*  Register the Automapper for dependency injection.
*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
* Since we are not using HTTPS implementation for this project
* the usage of UseHttpsRedirection is not required.
*/
//app.UseHttpsRedirection();

/*
* Microsoft.AspNetCore.Routing.EndpointMiddleware 
* will execute the Endpoint associated with the current request.
*/
app.UseRouting();
/*
* Mandatory for UseAuthorization method to appear in this sequence.
* Dotnet Framework:  The call to UseAuthorization should appear betw
* een app.UseRouting() and app.UseEndpoints(..) for authorization to 
* be correctly evaluated.
ed
*/
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

/*
* Stage In-Memory database data for our platform service.
*/
PrepDb.PrepPopulation(app, _env.IsProduction());
Console.WriteLine($"--> CommandService Endpoint {app.Configuration["CommandService"]}");

app.Run();


