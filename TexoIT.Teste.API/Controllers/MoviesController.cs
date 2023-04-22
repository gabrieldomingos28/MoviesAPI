using Microsoft.AspNetCore.Mvc;
using TexoIT.Teste.Domain.DataTransfer;
using TexoIT.Teste.Domain.Interfaces.Service;

namespace TexoIT.Teste.API.Controllers;

[Route("movies")]
public class MoviesController : Controller
{
    [HttpGet("producer-winners-intervals")]
    public IActionResult GetProducersWinnerIntervals([FromServices]IMovieService movieService)
    {
        List<PremiumProceducerDTO> producersWin = movieService.GetProducersWinInterval();
        int minInterval = producersWin.Min(p => p.Interval);
        int maxInterval = producersWin.Max(p => p.Interval);
        
        return Ok(new
        {
            min = producersWin.Where(p=>p.Interval == minInterval),
            max = producersWin.Where(p=>p.Interval == maxInterval),
        });
    }
}