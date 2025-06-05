using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read
{
    public class GetAllTarefasUseCase : IGetAllTarefasUseCase
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IMapper _mapper;

        public GetAllTarefasUseCase(ITarefaReadRepository tarefaReadRepository, IMapper mapper)
        {
            _tarefaReadRepository = tarefaReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTarefaResponse>?> Execute(CancellationToken token = default)
        {
            var listaTarefasDomain = await _tarefaReadRepository.GetAll(token);

            var ListaTarefasResponse = _mapper.Map<IEnumerable<GetAllTarefaResponse>?>(listaTarefasDomain);

            if (ListaTarefasResponse != null)
                ListaTarefasResponse = ListaTarefasResponse.OrderByDescending(t => t.Status);

            return ListaTarefasResponse;
        }
    }
}
