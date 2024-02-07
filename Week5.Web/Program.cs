using Week5.Web.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(
      opt =>
    {
      opt.UseInMemoryDatabase("InMemoryDatabase");
    }
);

builder.Services.AddControllers();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
