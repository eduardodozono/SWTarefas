using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.EF;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.EF.GetAll
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
