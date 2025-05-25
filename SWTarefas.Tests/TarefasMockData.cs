using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess;

namespace SWTarefas.Tests
{
    public class TarefasMockData
    {
        public static async Task CreateTarefas(TarefasApiAppication appication, bool criar)
        {
            using (var scope = appication.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var tarefasContext = provider.GetRequiredService<SWTarefasContext>())
                {
                    await tarefasContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await tarefasContext.Tarefas.AddAsync(new Tarefa
                        {
                            Titulo = "Titulo",
                            Descricao = "Descricao",
                            DataConclusaoPrevista = new DateOnly(2025, 1, 1),
                            DataConclusaoRealizada = new DateOnly(2025, 1, 1),
                            Status = 2
                        });

                        await tarefasContext.Tarefas.AddAsync(new Tarefa
                        {
                            Titulo = "Titulo 2",
                            Descricao = "Descricao 2",
                            DataConclusaoPrevista = new DateOnly(2025, 2, 2),
                            DataConclusaoRealizada = new DateOnly(2025, 2, 2),
                            Status = 1
                        });

                        await tarefasContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
