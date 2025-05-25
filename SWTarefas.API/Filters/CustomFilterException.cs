using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SWTarefas.Application.Exceptions;
using SWTarefas.Resources.Resources;

namespace SWTarefas.API.Filters
{
    public class CustomFilterException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseCustomException)
                OnExceptionCustomHandler(context);
            else
                OnExceptionInternalServerErrorHandler(context);
        }

        public void OnExceptionCustomHandler(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case CustomBadRequestException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var exception = context.Exception as CustomBadRequestException;

                    context.Result = new BadRequestObjectResult(exception._errors);

                    break;
                default:
                    OnExceptionInternalServerErrorHandler(context);
                    break;
            }
        }

        public void OnExceptionInternalServerErrorHandler(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(SWTarefasMessagesExceptions.ErroDesconhecido);
        }
    }
}
