using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using TexoIT.Teste.Domain.DataTransfer;
using TexoIT.Teste.Domain.Interfaces.Repository;
using TexoIT.Teste.Domain.Interfaces.Service;
using TexoIT.Teste.Domain.Models;

namespace TexoIT.Teste.API.Services;

public class MovieService : IMovieService
{
    private readonly ILogger<MovieService> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IHostingEnvironment _hostingEnvironment;
    public MovieService(
        IMovieRepository movieRepository,
        ILogger<MovieService> logger,
        IHostingEnvironment hostingEnvironment
        )
    {
        _movieRepository = movieRepository;
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
    }
    public void LoadMovies()
    {
        _logger.LogInformation("===> Carregando dados em memória");
        List<Movie> movies = GetMoviesDataFromFile();
        if (movies.Count > 0)
        {
            _movieRepository.AddRange(movies);
        }
        _logger.LogInformation("===> Dados carregados em memória");
    }

    public List<PremiumProceducerDTO> GetProducersWinInterval()
    {
        
        List<Movie> movies = _movieRepository.GetMoviesWinner();
        List<PremiumProceducerDTO> premiumProceducer = new List<PremiumProceducerDTO>();
        
        foreach (var movie in movies.OrderBy(m=>m.Year))
        {
            string[] producers = movie.Producers.Split(" and ");
            foreach (var producer in producers)
            {
                var moviesWinnerInterval = movies.Where(m =>
                    m.Id != movie.Id
                    && m.Producers.Contains(producer.Trim())
                    && (m.Year - movie.Year) >= 0
                ).ToList();

                if (moviesWinnerInterval.Count > 0)
                {
                    foreach (var movieWinnerInterval in moviesWinnerInterval)
                    {
                        premiumProceducer.Add(new PremiumProceducerDTO()
                        {
                            Producer = producer,
                            Interval = movieWinnerInterval.Year - movie.Year,
                            PreviusWin = movie.Year,
                            FollowingWin = movieWinnerInterval.Year
                        
                        });  
                    }

                   
                }
            }
            
        }

        return premiumProceducer
             .OrderBy(p=>p.Interval)
             .ToList();
    }

  

    private List<Movie> GetMoviesDataFromFile()
    {
        List<Movie> movies = new List<Movie>();
        using (var reader = new StreamReader($"{_hostingEnvironment.ContentRootPath}/data/movielist.csv"))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   HasHeaderRecord = false,
                   Delimiter = ";"
               })
              )
        {
            int index = 0;
            while (csv.Read())
            {
                if (index == 0)
                {
                    index += 1;
                    continue;
                }
                bool winner = false;
                if (csv.GetField(4) != null && csv.GetField(4).Equals("yes"))
                    winner = true;

                movies.Add(new Movie()
                {
                    Year = Convert.ToInt32(csv.GetField(0)),
                    Title = csv.GetField(1),
                    Studios = csv.GetField(2),
                    Producers = csv.GetField(3),
                    Winner = winner
                });
            }
        }

        return movies;
    }
}