using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Vivigest_backend.Application.Interfaces.IAuth;
using Vivigest_backend.Infrastructure.Authentication;
using Vivigest_backend.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VivigestDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Vivigestconnection"));
});
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
