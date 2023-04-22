using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace TexoIT.Teste.API.Test;

public class ApiApplication:WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {

        builder.ConfigureServices(services =>
        {

        });

        return base.CreateHost(builder);
    }
}