using MathTutor.Infrastructure.Data;
using MathTutor.Application.Interfaces;
using MathTutor.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), 
        b => b.MigrationsAssembly("MathTutor.Infrastructure")));

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
