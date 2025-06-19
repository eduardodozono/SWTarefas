using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;

namespace SWTarefas.Application.UsesCases.TarefasUseCasesSite.UseCases.Write
{
    public class WriteTarefasUseCaseSite : IWriteTarefasUseCaseSite
    {
        private readonly ITarefaWriteRepository _tarefaWriteRepository;
        private readonly ITarefaDeleteRepository _tarefasDeleteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WriteTarefasUseCaseSite(ITarefaWriteRepository tarefaWriteRepository, ITarefaDeleteRepository tarefasDeleteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tarefaWriteRepository = tarefaWriteRepository;
            _tarefasDeleteRepository = tarefasDeleteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(TarefaViewModel tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            await _tarefaWriteRepository.Create(tarefaDomain, token);

            await _unitOfWork.Commit(token);
        }

        public async Task Update(TarefaViewModel tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            _tarefaWriteRepository.Update(tarefaDomain);

            await _unitOfWork.Commit(token);
        }

        public async Task Delete(int tarefaId, CancellationToken token = default)
        {
            await _tarefasDeleteRepository.Delete(tarefaId, token);

            await _unitOfWork.Commit(token);
        }

    }
}
