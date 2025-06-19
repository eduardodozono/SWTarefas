namespace SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas
{
    public interface ITarefaDeleteDapperRepository
    {
        public Task Delete(int TarefaId, CancellationToken token = default);
    }
}
