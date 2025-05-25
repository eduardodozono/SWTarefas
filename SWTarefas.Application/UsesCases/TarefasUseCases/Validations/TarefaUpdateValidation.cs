using FluentValidation;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Resources.Resources;

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
            RuleFor(r => r.DataConclusaoPrevista).LessThanOrEqualTo(x => x.DataConclusaoRealizada ).WithMessage(SWTarefasMessagesExceptions.DataConclusaoInferiorDataPrevista);
            Include(new TarefaBaseValidation());
        }

        public async Task<bool> ExisteTarefaId(int TarefaId, CancellationToken token = default)
        {
            return (await _tarefaReadRepository.GetById(TarefaId, token) != null);
        }
    }
}
