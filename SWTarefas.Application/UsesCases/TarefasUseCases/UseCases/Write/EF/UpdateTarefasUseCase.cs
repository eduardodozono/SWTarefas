using AutoMapper;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.EF
{
    public class UpdateTarefasUseCase : IUpdateTarefasUseCase
    {
        private readonly ITarefaWriteRepository _tarefaWriteRepository;
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTarefasUseCase(ITarefaWriteRepository tarefaWriteRepository, ITarefaReadRepository tarefaReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tarefaWriteRepository = tarefaWriteRepository;
            _tarefaReadRepository = tarefaReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateTarefaResponse> Execute(UpdateTarefaRequest tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            var tarefaExisteDB = _tarefaReadRepository.GetById(tarefa.TarefaId);

            if (tarefaExisteDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            await ValidatorBaseUpdate.Validate(tarefa, token);

            _tarefaWriteRepository.Update(tarefaDomain);

            await _unitOfWork.Commit(token);

            return _mapper.Map<UpdateTarefaResponse>(tarefaDomain);
        }
    }
}
