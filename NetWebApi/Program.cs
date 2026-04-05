using Microsoft.EntityFrameworkCore;
using NetWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=NetPractice;Trusted_Connection=True;TrustServerCertificate=true;"));

builder.Services.AddScoped<AppDbContext>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("swagger/index.html", permanent: false);
        return;
    }
    await next();
});
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetWebApi"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
