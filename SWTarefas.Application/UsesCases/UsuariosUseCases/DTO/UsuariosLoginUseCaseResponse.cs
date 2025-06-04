namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class UsuariosLoginUseCaseResponse
    {
        public string AcessToken { get; init; } = string.Empty;

        public UsuariosLoginUseCaseResponse(string acessToken)
        {
            AcessToken = acessToken;
        }       
    }
}
