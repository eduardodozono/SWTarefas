using FluentValidation;
using SWTarefas.Resources.Resources;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    internal class TarefaUpdateValidation : AbstractValidator<UpdateTarefaRequest>
    {
        public TarefaUpdateValidation()
        {
            RuleFor(r => r.TarefaId).NotEmpty().WithMessage(SWTarefasMessagesExceptions.TarefaVazia);
            RuleFor(r => r.DataConclusaoPrevista).LessThanOrEqualTo(r => r.DataConclusaoRealizada)
                .When(r => r.DataConclusaoRealizada.HasValue && r.DataConclusaoPrevista.HasValue).WithMessage(SWTarefasMessagesExceptions.DataConclusaoInferiorDataPrevista);
            RuleFor(r => r.Status).Equal((int)TarefaStatus.Concluída)
                .When(x => x.DataConclusaoRealizada.HasValue).WithMessage(SWTarefasMessagesExceptions.StatusConcluidoErroApiDataRealizada);
            RuleFor(r => r.Status).Equal((int)TarefaStatus.Pendente)
                .When(x => !x.DataConclusaoRealizada.HasValue).WithMessage(SWTarefasMessagesExceptions.StatusPendenteErroApiDataRealizadaVazia);

            Include(new TarefaBaseValidation());
        }
    }
}
