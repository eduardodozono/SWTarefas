using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Validations;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.ValidatorBase
{
    public static class UsuarioLoginValidator
    {
        public static async Task Validate(UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var validation = new UsuarioLoginBaseValidation();

            var resultValidation = await validation.ValidateAsync(request, token);

            if (!resultValidation.IsValid)
                throw new CustomBadRequestException(resultValidation.Errors.Select(erros => erros.ErrorMessage).ToList());
        }
    }
}
