using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SWTarefas.Domain.DTO.Tarefas;
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
            var tarefaDB = await _sWTarefasContext.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.TarefaId.Equals(id), token);

            return tarefaDB;
        }

        public async Task<IEnumerable<Tarefa>?> Filter(FilterTarefa filter, CancellationToken token = default)
        {
            var queryFilter = _sWTarefasContext.Tarefas.Where(f => f.TarefaId > 0);

            if (filter.TarefaId > 0)
                queryFilter = queryFilter.Where(f => f.TarefaId.Equals(filter.TarefaId));

            if (filter.Status > 0)
                queryFilter = queryFilter.Where(f => f.Status.Equals(filter.Status));

            if (!string.IsNullOrWhiteSpace(filter.Titulo))
                queryFilter = queryFilter.Where(f => f.Titulo.Contains(filter.Titulo));

            if (!string.IsNullOrWhiteSpace(filter.Descricao))
                queryFilter = queryFilter.Where(f => f.Titulo.Contains(filter.Descricao));

            if (filter.DataConclusaoPrevista != null)
                queryFilter = queryFilter.Where(f => f.DataConclusaoPrevista.Equals(filter.DataConclusaoPrevista));

            if (filter.DataConclusaoRealizada != null)
                queryFilter = queryFilter.Where(f => f.DataConclusaoRealizada.Equals(filter.DataConclusaoRealizada));

            return await queryFilter.ToListAsync(token);
        }
    }
}
