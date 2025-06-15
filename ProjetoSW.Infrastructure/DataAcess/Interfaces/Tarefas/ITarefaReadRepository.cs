using System.Linq.Expressions;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas
{
    public interface ITarefaReadRepository
    {
        public Task<IEnumerable<Tarefa>?> GetAll(CancellationToken token = default);
        public IEnumerable<Tarefa>? GetAll(Expression<Func<Tarefa, bool>> filter);
        public Task<Tarefa?> GetById(int id, CancellationToken token = default);
        public Task<IEnumerable<Tarefa>?> Filter(FilterTarefa filter, CancellationToken token = default);
    }
}
