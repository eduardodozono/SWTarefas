namespace SWTarefas.Application.Exceptions
{
    public class BaseCustomException : SystemException
    {
        public BaseCustomException(string message) : base(message) { }
    }
}
