using System.Net;

namespace SWTarefas.Application.Exceptions
{
    public abstract class BaseCustomException : SystemException
    {
        public BaseCustomException(string message) : base(message) { }

        public abstract HttpStatusCode GetStatusCode();

        public abstract IList<string> GetErrors();
    }
}
