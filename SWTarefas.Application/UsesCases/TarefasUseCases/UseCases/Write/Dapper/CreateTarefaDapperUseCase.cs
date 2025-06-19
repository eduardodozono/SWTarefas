using AutoMapper;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.Dapper
{
    public class CreateTarefaDapperUseCase : ICreateTarefaDapperUseCase
    {
        private readonly ITarefaWriteDapperRepository tarefaWriteDapperRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkDapper _unitOfWorkDapper;

        public CreateTarefaDapperUseCase(ITarefaWriteDapperRepository tarefaWriteDapperRepository, IMapper mapper, IUnitOfWorkDapper unitOfWorkDapper)
        {
            this.tarefaWriteDapperRepository = tarefaWriteDapperRepository;
            _mapper = mapper;
            _unitOfWorkDapper = unitOfWorkDapper;
        }

        public async Task<CreateTarefaResponse> Execute(CreateTarefaRequest tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            await ValidatorBaseCreate.Validate(tarefa, token);

            _unitOfWorkDapper.BeginTransaction();

            var newTarefa = await tarefaWriteDapperRepository.Create(tarefaDomain, token);

            _unitOfWorkDapper.Commit();

            return _mapper.Map<CreateTarefaResponse>(newTarefa);
        }
    }
}
