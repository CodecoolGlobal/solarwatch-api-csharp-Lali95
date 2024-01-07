using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "apiWithAuthBackend",
            ValidAudience = "apiWithAuthBackend",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("!SomethingSecret!")
            ),
        };
    });

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
app.UseAuthentication();
app.UseAuthorization();

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

// Example: User login logic (replace this with your actual logic)
app.MapPost("/login", async context =>
{
    var username = context.Request.Form["username"];
    var password = context.Request.Form["password"];

    // Example: Check credentials (replace this with your actual user authentication logic)
    if (IsValidUser(username, password))
    {
        var token = GenerateJwtToken(username);
        await context.Response.WriteAsync(token);
    }
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid credentials");
    }
});

app.Run();

// Example: Validate user credentials (replace this with your actual logic)
bool IsValidUser(string username, string password)
{
    // Implement your user authentication logic here
    // For example, check against a database or external service
    return username == "example" && password == "password";
}

// Example: Generate JWT token (replace this with your actual logic)
string GenerateJwtToken(string username)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!SomethingSecret!"));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "apiWithAuthBackend",
        audience: "apiWithAuthBackend",
        claims: new[] { new Claim(ClaimTypes.Name, username) },
        expires: DateTime.Now.AddHours(1), // Token expiration time
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
