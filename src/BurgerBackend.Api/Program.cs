using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using BurgerBackend.Domain.Repositories.Cosmos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Host.ConfigureAppConfiguration(options =>
{
    options.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
})
.ConfigureServices((hostBuilder, services) =>
{
    services.Configure<CosmosConfiguration>(hostBuilder.Configuration.GetSection(nameof(CosmosConfiguration)));

    services.AddScoped(s =>
    {
        var cosmosConnectionString = s.GetRequiredService<IOptions<CosmosConfiguration>>().Value.ConnectionString;
        var clientBuilder = new CosmosClientBuilder(cosmosConnectionString);
        return clientBuilder.Build();
    });

    services.AddScoped<IBurgerPlacesRepository, BurgerPlacesRepository>();
    services.AddScoped<IGetAllPlacesHandler, GetAllPlacesHandler>();
    services.AddScoped<IGetPlaceByIdHandler, GetPlaceByIdHandler>();
    services.AddScoped<IGetReviewsByPlaceIdHandler, GetReviewsByPlaceIdHandler>();
    services.AddScoped<IGetReviewByIdHandler, GetReviewByIdHandler>();
    services.AddScoped<ICreateReviewHandler, CreateReviewHandler>();
    services.AddScoped<IUpdateReviewHandler, UpdateReviewHandler>();
    services.AddScoped<IDeleteReviewHandler, DeleteReviewHandler>();

    services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Information);
            logging.AddConsole();
            logging.AddDebug();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
