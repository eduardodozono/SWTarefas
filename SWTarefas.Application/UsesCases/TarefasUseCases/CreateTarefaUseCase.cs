using AutoMapper;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCases.Validations;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases
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

            await Validate(tarefa, token);

            await _tarefaWriteRepository.Create(tarefaDomain, token);

            await _unitOfWork.Commit(token);

            return _mapper.Map<CreateTarefaResponse>(tarefaDomain);
        }

        public async Task Validate(CreateTarefaRequest tarefa, CancellationToken token = default)
        {
            var validator = new TarefaBaseValidation();

            var result = await validator.ValidateAsync(tarefa, token);

            if (!result.IsValid)
                throw new CustomBadRequestException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
