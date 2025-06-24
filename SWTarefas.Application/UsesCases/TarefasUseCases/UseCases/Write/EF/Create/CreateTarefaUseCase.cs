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

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.EF.Create
{
    public class CreateTarefaUseCase : ICreateTarefaUseCase
    {
        private readonly ITarefaWriteRepository _tarefaWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTarefaUseCase(ITarefaWriteRepository tarefaWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tarefaWriteRepository = tarefaWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateTarefaResponse> Execute(CreateTarefaRequest tarefa, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(tarefa);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            await ValidatorBaseCreate.Validate(tarefa, token);

            await _tarefaWriteRepository.Create(tarefaDomain, token);

            await _unitOfWork.Commit(token);

            return _mapper.Map<CreateTarefaResponse>(tarefaDomain);
        }
    }
}
