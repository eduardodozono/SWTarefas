using FluentValidation;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.Validations.Shared;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    public class FilterTarefasValidator : AbstractValidator<FilterTarefaRequest>
    {
        public FilterTarefasValidator()
        {
            RuleFor(r => r.Titulo).SetValidator(new TituloCustomValidator<FilterTarefaRequest>());
            RuleFor(r => r.Descricao).SetValidator(new DescricaoCustomValidator<FilterTarefaRequest>());
            When(r => r.Status > 0, () =>
            {
                RuleFor(r => r.Status).SetValidator(new StatusCustomValidator<FilterTarefaRequest>());
            });
        }
    }
}
