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
            if (!configuration.IsUnitTestEnviroment())
            {
                // Para utlizar o banco de dados SQL Server descomentar a linha abaixo e comentar a linha do banco em memoria
                //services.AddDbContext<SWTarefasContext>(sql => sql.UseSqlServer(configuration.GetConnectionString("Connection")));

                // Banco de dados em memoria foi colocado somente para facilitar os testes mas o real seria o Sql Server
                services.AddDbContext<SWTarefasContext>(sql => sql.UseInMemoryDatabase(configuration.GetConnectionString("InMemory")!));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITarefaWriteRepository, TarefaRepository>();
            services.AddScoped<ITarefaReadRepository, TarefaRepository>();
            services.AddScoped<ITarefaDeleteRepository, TarefaRepository>();

            return services;
        }

        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            if(configuration.GetSection("InMemoryTest").Value == null)
                return false;

            return Boolean.Parse(configuration.GetSection("InMemoryTest").Value!);
        }

    }
}
