using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SWTarefas.Infrastructure.DataAcess;

namespace SWTarefas.Tests.Tests
{
    public class TarefasApiAppication : WebApplicationFactory<ProgramApi>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var database = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<SWTarefasContext>));

                services.AddDbContext<SWTarefasContext>(options => options.UseInMemoryDatabase("ad9a8736-bd66-4ffc-ba4e-6a837bd003c7", database).EnableServiceProviderCaching(false));
            });

            return base.CreateHost(builder);
        }
    }
}
