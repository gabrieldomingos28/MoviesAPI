using TexoIT.Teste.Domain.Interfaces.Repository;
using TexoIT.Teste.Domain.Models;

namespace TexoIT.Teste.Repository;

public class MovieRepository:IMovieRepository
{
    private readonly DataBaseContext _context;

    public MovieRepository(DataBaseContext context)
    {
        _context = context;
    }
    public void AddRange(List<Movie> movies)
    {
        _context.Movies.AddRange(movies);
        _context.SaveChanges();
    }

    public List<Movie> GetMoviesWinner()
    {
        return _context
            .Movies
            .Where(movie=> movie.Winner == true)
            .ToList();
    }
}