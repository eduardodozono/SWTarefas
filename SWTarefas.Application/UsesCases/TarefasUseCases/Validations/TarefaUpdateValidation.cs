using FluentValidation;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Resources.Resources;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    internal class TarefaUpdateValidation : AbstractValidator<UpdateTarefaRequest>
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;

        public TarefaUpdateValidation(ITarefaReadRepository tarefaReadRepository)
        {
            _tarefaReadRepository = tarefaReadRepository;

            RuleFor(r => r.TarefaId).NotEmpty().WithMessage(SWTarefasMessagesExceptions.TarefaVazia);
            RuleFor(r => r.TarefaId).MustAsync(ExisteTarefaId).WithMessage(SWTarefasMessagesExceptions.TarefaNaoExiste);
            RuleFor(r => r.DataConclusaoPrevista).LessThanOrEqualTo(r => r.DataConclusaoRealizada)
                .When(r => r.DataConclusaoRealizada.HasValue && r.DataConclusaoPrevista.HasValue).WithMessage(SWTarefasMessagesExceptions.DataConclusaoInferiorDataPrevista);
            RuleFor(r => r.Status).Equal((int)TarefaStatus.Concluída)
                .When(x => x.DataConclusaoRealizada.HasValue).WithMessage(SWTarefasMessagesExceptions.StatusConcluidoErroApiDataRealizada);
            RuleFor(r => r.Status).Equal((int)TarefaStatus.Pendente)
                .When(x => !x.DataConclusaoRealizada.HasValue).WithMessage(SWTarefasMessagesExceptions.StatusPendenteErroApiDataRealizadaVazia);

            Include(new TarefaBaseValidation());
        }

        public async Task<bool> ExisteTarefaId(int TarefaId, CancellationToken token = default)
        {
            return (await _tarefaReadRepository.GetById(TarefaId, token) != null);
        }
    }
}
