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
            if (context.Exception is BaseCustomException customException)
                OnExceptionCustomHandler(context, customException);
            else
                OnExceptionInternalServerErrorHandler(context);
        }

        public void OnExceptionCustomHandler(ExceptionContext context, BaseCustomException customException)
        {
            context.HttpContext.Response.StatusCode = (int)customException.GetStatusCode();

            context.Result = new ObjectResult(customException.GetErrors());
        }

        public void OnExceptionInternalServerErrorHandler(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(SWTarefasMessagesExceptions.ErroDesconhecido);
        }
    }
}
