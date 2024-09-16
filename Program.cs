using ComicsAPI.Data;
using ComicsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Dependency Injection for PasswordService
builder.Services.AddSingleton<PasswordService>();

// Swagger configuration for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
      Title = "API de Comics",
      Version = "v1",
      Description = "API para la gestión de productos de una tienda de cómics",
      Contact = new OpenApiContact
      {
        Name = "Soporte de ComicsAPI",
        Email = "soporte@example.com",
        Url = new Uri("https://example.com")
      }
    });
  }
);
// CORS policy to allow all origins, methods and headers
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
      builder =>
      {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
      });
});
// Database configuration with SQLite and Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Registering the ProductosController
builder.Services.AddControllers();
var app = builder.Build();
// Swagger configuration for API documentation
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
