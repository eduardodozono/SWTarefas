using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.Validations;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.NovaPasta
{
    public static class ValidatorBaseFilter
    {
        public static async Task Validate(FilterTarefaRequest request)
        {
            var validator = new FilterTarefasValidator();

            var resultValidator = await validator.ValidateAsync(request);

            if (!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.Select(x => x.ErrorMessage).Distinct().ToList();

                throw new CustomBadRequestException(errors);
            }
        }
    }
}
