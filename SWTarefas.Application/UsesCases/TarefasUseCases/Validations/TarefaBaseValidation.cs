using FluentValidation;
using SWTarefas.Resources.Resources;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations
{
    internal class TarefaBaseValidation : AbstractValidator<TarefaBaseUseCase>
    {
        public TarefaBaseValidation()
        {
            RuleFor(r => r.Titulo).NotEmpty().WithMessage(SWTarefasMessagesExceptions.TituloVazio);
            RuleFor(r => r.Titulo).MaximumLength(100).WithMessage(SWTarefasMessagesExceptions.TItuloMaximoCaracteres );
            RuleFor(r => r.Descricao).MaximumLength(400).WithMessage(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
            RuleFor(r => r.DataConclusaoPrevista).NotEmpty().WithMessage(SWTarefasMessagesExceptions.ErroDataPrevistaVazia);
            RuleFor(r => r.Status).NotEmpty().WithMessage(SWTarefasMessagesExceptions.StatusVazio);
            RuleFor(r => r.Status).Must(s => Enum.IsDefined(typeof(TarefaStatus), s)).WithMessage(SWTarefasMessagesExceptions.StatusInvalido);
        }
    }
}
