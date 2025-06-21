using AutoMapper;
using MediatR;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.MediatR.DTO.Request;
using SWTarefas.Application.UsesCases.MediatR.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write
{
    public class CreateTarefaHandler : IRequestHandler<CreateTarefaCommandRequest, CreateTarefaMResponse>
    {
        private readonly ITarefaWriteRepository _tarefaWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateTarefaHandler(ITarefaWriteRepository tarefaWriteRepository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _tarefaWriteRepository = tarefaWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CreateTarefaMResponse> Handle(CreateTarefaCommandRequest request, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(request);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            await ValidatorBaseCreate.Validate(request, token);

            await _tarefaWriteRepository.Create(tarefaDomain, token);

            await _unitOfWork.Commit(token);

            await _mediator.Publish(new CreateTarefaNotification(tarefaDomain));

            return await Task.FromResult(_mapper.Map<CreateTarefaMResponse>(tarefaDomain));
        }
    }
}
