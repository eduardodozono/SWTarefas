using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Validations;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.ValidatorBase
{
    public static class UsuarioLoginCreateValidator
    {
        public static async Task Validate(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var validation = new UsuarioLoginBaseValidation();

            var result = await validation.ValidateAsync(request);

            if (!result.IsValid)
                throw new CustomBadRequestException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
