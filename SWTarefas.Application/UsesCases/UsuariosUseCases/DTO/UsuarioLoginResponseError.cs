namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class UsuarioLoginResponseError
    {
        public IList<string> _errorMessage { get; set; }

        public bool TokenIsExpired { get; set; }

        public UsuarioLoginResponseError(IList<string> errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public UsuarioLoginResponseError(string error)
        {
            _errorMessage = new List<string>() { error };
        }
    }
}
