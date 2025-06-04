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

            await Validate(tarefa, token);

            _tarefaWriteRepository.Update(tarefaDomain);

            await _unitOfWork.Commit(token);

            return _mapper.Map<UpdateTarefaResponse>(tarefaDomain);
        }

        public async Task Validate(UpdateTarefaRequest tarefa, CancellationToken token = default)
        {
            var validator = new TarefaUpdateValidation(_tarefaReadRepository);

            var result = await validator.ValidateAsync(tarefa, token);

            if (!result.IsValid)
                throw new CustomBadRequestException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
