using Microsoft.EntityFrameworkCore;
using Projet_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données PostgreSQL avec Entity Framework Core
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//  CORS : autoriser tous les appels HTTP (à restreindre en production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Ajout des services MVC et Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Utilisation du CORS avant les endpoints
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
