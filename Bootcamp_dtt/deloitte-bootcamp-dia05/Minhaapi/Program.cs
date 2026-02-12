using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Services; 
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<LoteService>();

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection"); 
    options.UseNpgsql(cs);
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connectionString);
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
