using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.NovaPasta;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.Dapper.Filter
{
    public class FilterTarefasDapperUseCase : IFilterTarefasDapperUseCase
    {
        private readonly ITarefaReadDapperRepository _tarefaReadDapperRepository;
        private readonly IMapper _mapper;

        public FilterTarefasDapperUseCase(ITarefaReadDapperRepository tarefaReadDapperRepository, IMapper mapper)
        {
            _tarefaReadDapperRepository = tarefaReadDapperRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTarefaResponse>?> Execute(FilterTarefaRequest request, CancellationToken token = default)
        {
            await ValidatorBaseFilter.Validate(request);

            var filterDTO = _mapper.Map<FilterTarefa>(request);

            var listaTarefasDomain = await _tarefaReadDapperRepository.Filter(filterDTO);

            var listaTarefaResult = _mapper.Map<IEnumerable<GetAllTarefaResponse>?>(listaTarefasDomain);

            return listaTarefaResult;
        }
    }
}
