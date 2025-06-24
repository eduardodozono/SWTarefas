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

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.Dapper.Update
{
    public class UpdateTarefasDapperUseCase: IUpdateTarefasDapperUseCase
    {
        private readonly ITarefaReadDapperRepository _tarefaReadDapperRepository;
        private readonly ITarefaWriteDapperRepository _tarefaWriteDapperRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkDapper _unitOfWorkDapper;

        public UpdateTarefasDapperUseCase(ITarefaReadDapperRepository tarefaReadDapperRepository, ITarefaWriteDapperRepository tarefaWriteDapperRepository, IMapper mapper, IUnitOfWorkDapper unitOfWorkDapper)
        {
            _tarefaReadDapperRepository = tarefaReadDapperRepository;
            _tarefaWriteDapperRepository = tarefaWriteDapperRepository;
            _mapper = mapper;
            _unitOfWorkDapper = unitOfWorkDapper;
        }

        public async Task<UpdateTarefaResponse> Execute(UpdateTarefaRequest tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            var tarefaExisteDB = await _tarefaReadDapperRepository.GetById(tarefa.TarefaId);

            if (tarefaExisteDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            await ValidatorBaseUpdate.Validate(tarefa, token);

            _unitOfWorkDapper.BeginTransaction();

            await _tarefaWriteDapperRepository.Update(tarefaDomain);

            _unitOfWorkDapper.Commit();

            return _mapper.Map<UpdateTarefaResponse>(tarefaDomain);
        }
    }
}
