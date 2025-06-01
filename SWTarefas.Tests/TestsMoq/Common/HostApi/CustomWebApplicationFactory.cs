using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Infrastructure.DataAcess;
using SWTarefas.Tests.TestsMoq.Common.Entities;

namespace SWTarefas.Tests.TestsMoq.Common.HostApi
{
    public class CustomWebApplicationFactory : WebApplicationFactory<ProgramApi>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(
                services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SWTarefasContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<SWTarefasContext>(op =>
                    {
                        op.UseInMemoryDatabase("InMemoryDbForTesting");

                        op.UseInternalServiceProvider(provider);

                        op.EnableServiceProviderCaching(false);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<SWTarefasContext>();

                    dbContext.Database.EnsureDeleted();

                    dbContext.Database.EnsureCreated();
                });

            base.ConfigureWebHost(builder);
        }
    }
}
