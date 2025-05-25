using FluentValidation;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    internal class TarefaUpdateValidation : AbstractValidator<UpdateTarefaRequest>
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;

        public TarefaUpdateValidation(ITarefaReadRepository tarefaReadRepository)
        {
            _tarefaReadRepository = tarefaReadRepository;

            RuleFor(r => r.TarefaId).NotEmpty().WithMessage("O campo tarefaid não pode fica vazio.");
            RuleFor(r => r.TarefaId).MustAsync(ExisteTarefaId).WithMessage("A tarefa não existe");
            RuleFor(r => r.DataConclusaoPrevista).LessThanOrEqualTo(x => x.DataConclusaoRealizada ).WithMessage("A data de conclusão prevista tem que ser inferior ou igual a data de conclusão realizada.");
            Include(new TarefaBaseValidation());
        }

        public async Task<bool> ExisteTarefaId(int TarefaId, CancellationToken token = default)
        {
            return (await _tarefaReadRepository.GetById(TarefaId, token) != null);
        }
    }
}
