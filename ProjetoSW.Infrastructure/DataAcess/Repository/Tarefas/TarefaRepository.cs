using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Infrastructure.DataAcess.Repository.Tarefas
{
    public class TarefaRepository : ITarefaDeleteRepository, ITarefaReadRepository, ITarefaWriteRepository
    {
        private readonly SWTarefasContext _sWTarefasContext;

        public TarefaRepository(SWTarefasContext sWTarefasContext)
        {
            _sWTarefasContext = sWTarefasContext;
        }

        public async Task<Tarefa> Create(Tarefa tarefa, CancellationToken token = default)
        {
            await _sWTarefasContext.Tarefas.AddAsync(tarefa, token);

            return tarefa;
        }
        public Tarefa Update(Tarefa tarefa)
        {
            _sWTarefasContext.Tarefas.Update(tarefa);

            return tarefa;
        }

        public async Task Delete(int id, CancellationToken token = default)
        {
            var tarefaDB = await GetById(id, token);

            if (tarefaDB != null)
                _sWTarefasContext.Tarefas.Remove(tarefaDB);
        }

        public async Task<IEnumerable<Tarefa>?> GetAll(CancellationToken token = default)
        {
            var listaTarefas = await _sWTarefasContext.Tarefas.AsNoTracking().OrderByDescending(t => t.Status).ToListAsync(token);

            return listaTarefas;
        }

        public IEnumerable<Tarefa>? GetAll(Expression<Func<Tarefa, bool>> filter)
        {
            var listaTarefas = _sWTarefasContext.Tarefas.AsNoTracking().Where(filter).OrderByDescending(t => t.Status).ToList();

            return listaTarefas;
        }

        public async Task<Tarefa?> GetById(int id, CancellationToken token = default)
        {
            var tarefaDB = await _sWTarefasContext.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.TarefaId == id, token);

            return tarefaDB;
        }
    }
}
