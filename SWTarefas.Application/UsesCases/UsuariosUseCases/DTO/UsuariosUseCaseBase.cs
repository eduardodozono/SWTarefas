namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public abstract class UsuariosUseCaseBase
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;

        public UsuariosUseCaseBase() { }

        public UsuariosUseCaseBase(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
