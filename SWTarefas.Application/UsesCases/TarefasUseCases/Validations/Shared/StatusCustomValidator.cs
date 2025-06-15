using FluentValidation;
using FluentValidation.Validators;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Validations.Shared
{
    public class StatusCustomValidator<T> : PropertyValidator<T, int>
    {
        public override string Name => "StatusCustomValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{ErrorMessage}";
        }

        public override bool IsValid(ValidationContext<T> context, int value)
        {
            if (value != 1 && value != 2)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", SWTarefasMessagesExceptions.StatusInvalido);

                return false;
            }

            return true;
        }
    }
}
