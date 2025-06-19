using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.NovaPasta;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.EF
{
    public class FilterTarefasUseCase : IFilterTarefasUseCase
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IMapper _mapper;

        public FilterTarefasUseCase(ITarefaReadRepository tarefaReadRepository, IMapper mapper)
        {
            _tarefaReadRepository = tarefaReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTarefaResponse>?> Execute(FilterTarefaRequest request, CancellationToken token = default)
        {
            await ValidatorBaseFilter.Validate(request);

            var filterDTO = _mapper.Map<FilterTarefa>(request);

            var resultDomain = await _tarefaReadRepository.Filter(filterDTO, token);

            var resultResponse = _mapper.Map<IEnumerable<GetAllTarefaResponse>?>(resultDomain);

            return resultResponse;
        }
    }
}
