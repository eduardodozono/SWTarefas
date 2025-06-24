using System.Diagnostics;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Jobs.Jobs.Tarefas
{
    public static class TarefasNotificacaoJob
    {
        public static async Task Created(Tarefa tarefa)
        {
            Debug.WriteLine($"Iniciando job : ({tarefa.Titulo}).");

            await Task.CompletedTask;
        }
    }
}
