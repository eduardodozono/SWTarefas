using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoSW.Infrastructure.DataAcess.Repository.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.Repository.Tarefas;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static IServiceCollection AddInfraExtension(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            //services.AddDbContext<SWTarefasContext>(sql => sql.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddDbContext<SWTarefasContext>(sql => sql.UseInMemoryDatabase(configuration.GetConnectionString("InMemory")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITarefaWriteRepository, TarefaRepository>();
            services.AddScoped<ITarefaReadRepository, TarefaRepository>();
            services.AddScoped<ITarefaDeleteRepository, TarefaRepository>();

            return services;
        }
    }
}
