using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess;
using SWTarefas.Tests.Tests;

namespace SWTarefas.Tests.TestsMoq.Common.HostApi
{
    public static class TarefasDataBaseUtils
    {
        public static async Task CreateTarefas(CustomWebApplicationFactory appication, List<Tarefa>? listaTarefas)
        {
            var provider = appication.Services;

            var tarefasContext = provider.GetRequiredService<SWTarefasContext>();

            await tarefasContext.Database.EnsureCreatedAsync();

            if (tarefasContext != null && listaTarefas != null && listaTarefas.Count > 0)
            {
                await tarefasContext.Tarefas.AddRangeAsync(listaTarefas);

                await tarefasContext.SaveChangesAsync();
            }
        }
    }
}
