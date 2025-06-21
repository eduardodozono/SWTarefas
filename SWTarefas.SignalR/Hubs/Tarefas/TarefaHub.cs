using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;

[assembly: InternalsVisibleTo("SWTarefas.API")]
namespace SWTarefas.SignalR.Hubs.Tarefas
{
    internal class TarefaHub : Hub
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;

        public TarefaHub(ITarefaReadRepository tarefaReadRepository)
        {
            _tarefaReadRepository = tarefaReadRepository;
        }

        public async IAsyncEnumerable<Tarefa> ListaTarefasStreaming([EnumeratorCancellation] CancellationToken token)
        {
            while (true)
            {
                var listaTarefas = await _tarefaReadRepository.GetAll(token) ?? [];

                foreach (var tarefa in listaTarefas)
                {
                    yield return tarefa;
                }

                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
        }
    }
}
