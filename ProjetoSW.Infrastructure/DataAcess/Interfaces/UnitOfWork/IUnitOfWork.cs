namespace SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task Commit(CancellationToken token = default);
    }
}
