using SWTarefas.Infrastructure.DataAcess.EF;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;

namespace SWTarefas.Infrastructure.DataAcess.EF.Repository.UnitOfWork
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
