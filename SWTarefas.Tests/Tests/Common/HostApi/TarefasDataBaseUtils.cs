using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF;

namespace SWTarefas.Tests.TestsMoq.Common.HostApi
{
    public static class TarefasDataBaseUtils
    {
        public static async Task<SWTarefasContext> GetDbContext(CustomWebApplicationFactory appication)
        {
            var provider = appication.Services;

            var tarefasContext = provider.GetRequiredService<SWTarefasContext>();

            await tarefasContext.Database.EnsureCreatedAsync();

            return tarefasContext;
        }

        public static async Task CreateUsuarios(CustomWebApplicationFactory appication, List<Usuario>? listaUsuarios)
        {
            var tarefasContext = await GetDbContext(appication);

            if (tarefasContext != null && listaUsuarios != null && listaUsuarios.Count > 0)
            {
                await tarefasContext.Usuarios.AddRangeAsync(listaUsuarios);

                await tarefasContext.SaveChangesAsync();
            }
        }

        public static async Task CreateTarefas(CustomWebApplicationFactory appication, List<Tarefa>? listaTarefas)
        {
            var tarefasContext = await GetDbContext(appication);

            if (tarefasContext != null && listaTarefas != null && listaTarefas.Count > 0)
            {
                await tarefasContext.Tarefas.AddRangeAsync(listaTarefas);

                await tarefasContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteAllTarefas(CustomWebApplicationFactory appication)
        {
            var tarefasContext = await GetDbContext(appication);

            var listaTarefas = await tarefasContext.Tarefas.ToListAsync();

            if (tarefasContext != null && listaTarefas != null && listaTarefas.Count > 0)
            {
                tarefasContext.Tarefas.RemoveRange(listaTarefas);

                await tarefasContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteAllUsuarios(CustomWebApplicationFactory appication)
        {
            var tarefasContext = await GetDbContext(appication);

            var listaUsuarios = await tarefasContext.Usuarios.ToListAsync();

            if (tarefasContext != null && listaUsuarios != null && listaUsuarios.Count > 0)
            {
                tarefasContext.Usuarios.RemoveRange(listaUsuarios);

                await tarefasContext.SaveChangesAsync();
            }
        }
    }
}
