using System.Linq.Expressions;
using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCasesSite
{
    public class ReadTarefasUseCaseSite : IReadTarefasUseCaseSite
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IMapper _mapper;

        public ReadTarefasUseCaseSite(ITarefaReadRepository tarefaReadRepository, IMapper mapper)
        {
            _tarefaReadRepository = tarefaReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TarefaViewModel>?> GetAll(CancellationToken token = default)
        {
            var listaTarefasDomain = await _tarefaReadRepository.GetAll(token);

            var listaTarefasModel = _mapper.Map<IEnumerable<TarefaViewModel>?>(listaTarefasDomain);

            return listaTarefasModel;
        }

        public IEnumerable<TarefaViewModel>? GetAll(Expression<Func<Tarefa, bool>> filter)
        {
            var listaTarefasDomain = _tarefaReadRepository.GetAll(filter);

            var listaTarefasModel = _mapper.Map<IEnumerable<TarefaViewModel>?>(listaTarefasDomain);

            return listaTarefasModel;
        }

        public async Task<TarefaViewModel?> GetById(int tarefaId, CancellationToken token = default)
        {
            var tarefaDomain = await _tarefaReadRepository.GetById(tarefaId, token);

            var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefaDomain);

            return tarefaViewModel;
        }
    }
}
