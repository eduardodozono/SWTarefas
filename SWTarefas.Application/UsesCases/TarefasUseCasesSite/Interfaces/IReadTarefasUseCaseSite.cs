using System.Linq.Expressions;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces
{
    public interface IReadTarefasUseCaseSite
    {
        public Task<IEnumerable<TarefaViewModel>?> GetAll(CancellationToken token = default);
        public Task<TarefaViewModel?> GetById(int tarefaId, CancellationToken token = default);
        public IEnumerable<TarefaViewModel>? GetAll(Expression<Func<Tarefa, bool>> filter);
    }
}
