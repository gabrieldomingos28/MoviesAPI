using Microsoft.EntityFrameworkCore;
using TexoIT.Teste.API.Services;
using TexoIT.Teste.Domain.Interfaces.Repository;
using TexoIT.Teste.Domain.Interfaces.Service;
using TexoIT.Teste.Domain.Models;
using TexoIT.Teste.Repository;

var builder = WebApplication.CreateBuilder(args);
var serviceCollection = new ServiceCollection();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovieService, MovieService>()
    .AddScoped<IMovieRepository, MovieRepository>()
    .AddDbContext<DataBaseContext>(opt => opt.UseInMemoryDatabase("movies"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var serviceProvider = builder.Services.BuildServiceProvider();
var movieService = serviceProvider.GetService<IMovieService>();
movieService.LoadMovies();

app.Run();

public partial class Program
{
    
}