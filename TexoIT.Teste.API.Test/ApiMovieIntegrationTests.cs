using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using TexoIT.Teste.Domain.DataTransfer;

namespace TexoIT.Teste.API.Test;

public class ApiMovieIntegrationTests
{
    [Test]
    public async Task GET_Intervalos_Produtores_Vencedores()
    {
        await using var application = new ApiApplication();
        
        var url = "/movies/producer-winners-intervals";

        var client = application.CreateClient();

        var result = await client.GetAsync(url);
        var produdoresVencedores = await client.GetFromJsonAsync<PremiumProceducerReponse>("/movies/producer-winners-intervals");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotNull(produdoresVencedores);
        Assert.IsTrue(produdoresVencedores.min.Count >= 1);
        Assert.IsTrue(produdoresVencedores.max.Count >= 1);
    }
}