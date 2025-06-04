using FluentValidation;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Validations
{
    public class UsuarioLoginCreateValidation : AbstractValidator<CreateUsuariosLoginUseCaseRequest>
    {
        public UsuarioLoginCreateValidation()
        {
            RuleFor(r => r.Nome).Empty().WithMessage(SWTarefasMessagesExceptions.NomeVazio);
            Include(new UsuarioLoginBaseValidation());
        }
    }
}
