using TexoIT.Teste.Domain.DataTransfer;

namespace TexoIT.Teste.Domain.Interfaces.Service;

public interface IMovieService
{
    void LoadMovies();
    List<PremiumProceducerDTO> GetProducersWinInterval();

}