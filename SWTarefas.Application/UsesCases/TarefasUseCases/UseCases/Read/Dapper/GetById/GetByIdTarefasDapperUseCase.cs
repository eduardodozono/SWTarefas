using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.Dapper.GetById
{
    public class GetByIdTarefasDapperUseCase : IGetByIdTarefasDapperUseCase
    {
        private readonly ITarefaReadDapperRepository _tarefaReadDapperRepository;
        private readonly IMapper _mapper;

        public GetByIdTarefasDapperUseCase(ITarefaReadDapperRepository tarefaReadDapperRepository, IMapper mapper)
        {
            _tarefaReadDapperRepository = tarefaReadDapperRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdTarefaResponse?> Execute(int tarefaId, CancellationToken token = default)
        {
            var tarefaDomain = await _tarefaReadDapperRepository.GetById(tarefaId);

            return _mapper.Map<GetByIdTarefaResponse?>(tarefaDomain);
        }
    }
}
