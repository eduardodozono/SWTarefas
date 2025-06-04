using FluentValidation;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Validations
{
    public class UsuarioLoginValidation: AbstractValidator<UsuariosLoginUseCaseRequest>
    {
        public UsuarioLoginValidation()
        {
            RuleFor(t => t.Email).NotEmpty().WithMessage("O campo email é obrigatório.");
            RuleFor(t => t.Password).NotEmpty().WithMessage("O campo password é obrigatório.");
            RuleFor(t => t.Email).EmailAddress().WithMessage("O campo email é inválido.");
            RuleFor(t => t.Password).MinimumLength(6).WithMessage("O campo password é inválido.");
        }
    }
}
