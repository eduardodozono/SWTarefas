using AutoMapper;
using MediatR;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.MediatR.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write.Update
{
    public class UpdateTarefaHandler : IRequestHandler<UpdateTarefaCommandRequest, UpdateTarefaResponse>
    {
        private readonly ITarefaWriteRepository _tarefaWriteRepository;
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateTarefaHandler(ITarefaWriteRepository tarefaWriteRepository, ITarefaReadRepository tarefaReadRepository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _tarefaWriteRepository = tarefaWriteRepository;
            _tarefaReadRepository = tarefaReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdateTarefaResponse> Handle(UpdateTarefaCommandRequest request, CancellationToken token = default)
        {
            var tarefaDomain = _mapper.Map<Tarefa>(request);

            if (tarefaDomain == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.ProblemaConverterTarefa);

            var tarefaExisteDB = _tarefaReadRepository.GetById(request.TarefaId);

            if (tarefaExisteDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            await ValidatorBaseUpdate.Validate(request, token);

            _tarefaWriteRepository.Update(tarefaDomain);

            await _unitOfWork.Commit(token);

            await _mediator.Publish(new UpdateTarefaNotification(tarefaDomain));

            return await Task.FromResult(_mapper.Map<UpdateTarefaResponse>(tarefaDomain));
        }
    }
}
