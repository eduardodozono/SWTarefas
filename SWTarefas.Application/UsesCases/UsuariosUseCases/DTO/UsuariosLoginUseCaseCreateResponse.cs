namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class UsuariosLoginUseCaseCreateResponse
    {
        public string AcessToken { get; init; } = string.Empty;
        public string Nome { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        public UsuariosLoginUseCaseCreateResponse(string acessToken, string nome, string email)
        {
            AcessToken = acessToken;
            Nome = nome;
            Email = email;
        }        
    }
}
