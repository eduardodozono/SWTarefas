using FluentValidation;
using FluentValidation.Validators;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations.Shared
{
    public class TituloCustomValidator<T> : PropertyValidator<T, string?>
    {
        public override string Name => "TituloCustomValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{ErrorMessage}";
        }

        public override bool IsValid(ValidationContext<T> context, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                if (value.Length > 100)
                {
                    context.MessageFormatter.AppendArgument("ErrorMessage", SWTarefasMessagesExceptions.TItuloMaximoCaracteres);

                    return false;
                }

            return true;
        }
    }
}
