using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.Validations;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.ValidatorBase
{
    public static class ValidatorBaseUpdate
    {
        public static async Task Validate(UpdateTarefaRequest tarefa, CancellationToken token = default)
        {
            var validator = new TarefaUpdateValidation();

            var result = await validator.ValidateAsync(tarefa, token);

            if (!result.IsValid)
                throw new CustomBadRequestException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
