using FluentValidation;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Validations
{
    public class UsuarioLoginBaseValidation : AbstractValidator<UsuariosUseCaseBase>
    {
        public UsuarioLoginBaseValidation()
        {
            RuleFor(t => t.Email).NotEmpty().WithMessage(SWTarefasMessagesExceptions.EmailObrigatorio);
            RuleFor(t => t.Email).EmailAddress().WithMessage(SWTarefasMessagesExceptions.EmailInvalido);
            RuleFor(t => t.Password).NotEmpty().WithMessage(SWTarefasMessagesExceptions.PasswordObrigatorio);
            RuleFor(t => t.Password).MinimumLength(6).WithMessage(SWTarefasMessagesExceptions.PasswordInvalido);
        }
    }
}
