using FluentValidation;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    internal class TarefaBaseValidation : AbstractValidator<TarefaBaseUseCase>
    {
        public TarefaBaseValidation()
        {
            RuleFor(r => r.Titulo).NotEmpty().WithMessage("O campo titulo não pode fica vazio.");
            RuleFor(r => r.Titulo).MaximumLength(100).WithMessage("O campo titulo tem no máximo 100 cacteres.");
            RuleFor(r => r.Descricao).MaximumLength(400).WithMessage("O campo titulo tem no máximo 400 cacteres.");
            RuleFor(r => r.Status).NotEmpty().WithMessage("O campo status não pode fica vazio.");
            RuleFor(r => r.Status).Must(s => Enum.IsDefined(typeof(TarefaStatus), s)).WithMessage("Status inválido (1 - Concluída, 2 - Pendente)");
        }
    }
}
