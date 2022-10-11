using System.Text;
using Azure.Core;
using Azure.Identity;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Repositories.Blob;
using BurgerBackend.Domain.Repositories.Cosmos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureAppConfiguration(options =>
    {
        options.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", true, true)
            .AddEnvironmentVariables();
    })
.ConfigureServices((hostBuilder, services) =>
{
    services.Configure<CosmosConfiguration>(hostBuilder.Configuration.GetSection(nameof(CosmosConfiguration)));

    services.AddSingleton(s =>
    {
        var cosmosConnectionString = s.GetRequiredService<IOptions<CosmosConfiguration>>().Value.ConnectionString;
        var clientBuilder = new CosmosClientBuilder(cosmosConnectionString);
        return clientBuilder.Build();
    });

    services.AddAzureClients(clientBuilder =>
    {
        clientBuilder.AddBlobServiceClient(builder.Configuration.GetSection("AzureStorageConfiguration")).ConfigureOptions(
            options =>
            {
                options.Retry.Mode = RetryMode.Exponential;
                options.Retry.MaxDelay = TimeSpan.FromSeconds(60);
            });

        clientBuilder.ConfigureDefaults(builder.Configuration.GetSection("AzureDefaults"));

        clientBuilder.UseCredential(new DefaultAzureCredential());
    });

    services.AddSingleton<IBurgerPlacesRepository, BurgerPlacesRepository>();
    services.AddSingleton<IImagesRepository, ImagesRepository>();
    services.AddScoped<IGetAllPlacesHandler, GetAllPlacesHandler>();
    services.AddScoped<ICreatePlaceHandler, CreatePlaceHandler>();
    services.AddScoped<IGetPlaceByIdHandler, GetPlaceByIdHandler>();
    services.AddScoped<IGetReviewsByPlaceIdHandler, GetReviewsByPlaceIdHandler>();
    services.AddScoped<IGetReviewByIdHandler, GetReviewByIdHandler>();
    services.AddScoped<ICreateReviewHandler, CreateReviewHandler>();
    services.AddScoped<IUpdateReviewHandler, UpdateReviewHandler>();
    services.AddScoped<IDeleteReviewHandler, DeleteReviewHandler>();
    services.AddScoped<IAddImageHandler, AddImageHandler>();

    services.AddApplicationInsightsTelemetryWorkerService(options => options.InstrumentationKey = "ins key example");

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

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Burger Backend API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT", 
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7081",
            ValidAudience = "https://localhost:7081",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretkey@123"))
        };
    });

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
