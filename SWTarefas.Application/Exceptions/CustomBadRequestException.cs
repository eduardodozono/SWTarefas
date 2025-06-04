namespace SWTarefas.Application.Exceptions
{
    public class CustomBadRequestException : BaseCustomException
    {
        public readonly IList<string> _errors = new List<string>();

        public CustomBadRequestException(IList<string> errors) : base(string.Empty)
        {
            _errors = errors;
        }
        public CustomBadRequestException(string error) : base(string.Empty)
        {
            _errors.Add(error);
        }
    }
}
