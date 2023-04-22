using TexoIT.Teste.Domain.Models;

namespace TexoIT.Teste.Domain.Interfaces.Repository;

public interface IMovieRepository
{
    void AddRange(List<Movie> movies);
    List<Movie> GetMoviesWinner();

}