using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.Dapper
{
    public class GetAllTarefasDapperUseCase : IGetAllTarefasDapperUseCase
    {
        private readonly ITarefaReadDapperRepository _tarefaReadDapperRepository;
        private readonly IMapper _mapper;

        public GetAllTarefasDapperUseCase(ITarefaReadDapperRepository tarefaReadDapperRepository, IMapper mapper)
        {
            _tarefaReadDapperRepository = tarefaReadDapperRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTarefaResponse>?> Execute(CancellationToken token = default)
        {
            var teste = await _tarefaReadDapperRepository.GetTarefasUsuarios();

            var listaTarefasDomain = await _tarefaReadDapperRepository.GetAll(token);

            var ListaTarefasResponse = _mapper.Map<IEnumerable<GetAllTarefaResponse>?>(listaTarefasDomain);

            return ListaTarefasResponse;
        }
    }
}
