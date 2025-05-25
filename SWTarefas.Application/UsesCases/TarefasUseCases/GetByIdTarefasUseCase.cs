using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases
{
    public class GetByIdTarefasUseCase : IGetByIdTarefasUseCase
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IMapper _mapper;

        public GetByIdTarefasUseCase(ITarefaReadRepository tarefaReadRepository, IMapper mapper)
        {
            _tarefaReadRepository = tarefaReadRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdTarefaResponse?> Execute(int tarefaId, CancellationToken token = default)
        {
            var tarefaDomain = await _tarefaReadRepository.GetById(tarefaId, token);

            var tarefaRequest = _mapper.Map<GetByIdTarefaResponse?>(tarefaDomain);

            return tarefaRequest;
        }
    }
}
