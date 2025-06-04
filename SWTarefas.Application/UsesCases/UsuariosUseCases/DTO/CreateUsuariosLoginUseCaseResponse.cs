namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class CreateUsuariosLoginUseCaseResponse
    {
        public string AcessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public string Nome { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        public CreateUsuariosLoginUseCaseResponse() { }
        public CreateUsuariosLoginUseCaseResponse(string refreshtoken, string acessToken, string nome, string email)
        {
            AcessToken = acessToken;
            RefreshToken = refreshtoken;
            Nome = nome;
            Email = email;
        }
    }
}
