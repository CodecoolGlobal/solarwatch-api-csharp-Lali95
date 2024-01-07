using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SolarWatch.Data;
using SolarWatch.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=WeatherApi;User Id=sa;Password=Tt19372846519;TrustServerCertificate=true;"); 
});

builder.Services.AddHttpClient<SunriseSunsetRepository>();
builder.Services.AddScoped<SunriseSunsetRepository>();

var openWeatherMapApiKey = "8921956c7a9183a7c24b85d014c85aab";
builder.Services.AddSingleton(openWeatherMapApiKey);

builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolarWatch API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Use Swagger and SwaggerUI
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolarWatch API v1"));

app.UseRouting();

// Map the controllers and enable endpoint routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();