using Microsoft.AspNetCore.Http.Features;
using backend.Services;
using backend.Models; // If you need the Models namespace in Program.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; // To configure Swagger

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy to allow requests from your Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder.WithOrigins("http://localhost:4200")  // Your Angular app URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// MongoDB Connection Settings with null checks
var connectionString = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
var databaseName = builder.Configuration.GetValue<string>("DatabaseSettings:DatabaseName");

// If either value is null, throw an exception to avoid null reference issues
if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
{
    throw new InvalidOperationException("Database connection settings are missing in appsettings.json.");
}

// Register MovieService with the valid connection string and database name
builder.Services.AddSingleton<MovieService>(provider =>
    new MovieService(connectionString, databaseName)
);

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Mania API", Version = "v1" });
});

// Add support for file uploads and form data
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

var app = builder.Build();

// Enable CORS for your Angular frontend
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
