using SWTarefas.Infrastructure.DataAcess;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;

namespace ProjetoSW.Infrastructure.DataAcess.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SWTarefasContext _sWTarefasContext;

        public UnitOfWork(SWTarefasContext sWTarefasContext)
        {
            _sWTarefasContext = sWTarefasContext;
        }

        public async Task Commit(CancellationToken token = default)
        {
            await _sWTarefasContext.SaveChangesAsync(token);
        }
    }
}
