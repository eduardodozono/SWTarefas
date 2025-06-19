using SWTarefas.Domain.Entities;
using SWTarefas.Domain.DTO.Tarefas;


namespace SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas
{
    public interface ITarefaReadDapperRepository
    {
        public Task<IEnumerable<Tarefa>?> GetAll(CancellationToken token = default);
        public Task<Tarefa?> GetById(int tarefaId, CancellationToken token = default);
        public Task<IEnumerable<Tarefa>?> Filter(FilterTarefa request);
        public Task<TarefasUsuarios?> GetTarefasUsuarios(CancellationToken token = default);
    }
}
