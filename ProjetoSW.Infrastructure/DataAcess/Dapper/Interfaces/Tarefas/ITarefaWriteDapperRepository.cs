using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas
{
    public interface ITarefaWriteDapperRepository
    {
        public Task<Tarefa> Create(Tarefa tarefa, CancellationToken token = default);
        public Task<Tarefa> Update(Tarefa tarefa);
    }
}
