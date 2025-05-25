namespace SWTarefas.Application.Exceptions
{
    public class CustomBadRequestException : BaseCustomException
    {
        public readonly IList<string> _errors = new List<string>();

        public CustomBadRequestException(IList<string> errors)
        {
            _errors = errors;
        }
        public CustomBadRequestException(string error)
        {
            _errors.Add(error);
        }
    }
}
