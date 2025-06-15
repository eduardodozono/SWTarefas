using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAcessTokenValidator _acessTokenValidator;
        private readonly IUsuarioReadRepository _usuarioReadRepository;

        public AuthenticatedUserFilter(IAcessTokenValidator acessTokenValidator, IUsuarioReadRepository usuarioReadRepository)
        {
            _acessTokenValidator = acessTokenValidator;
            _usuarioReadRepository = usuarioReadRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                var userIdentifier = _acessTokenValidator.ValidateAndGetUserIdentifier(token);

                var existUser = await _usuarioReadRepository.ExistsActiveUserWithIdentifier(userIdentifier);

                if (!existUser)
                    throw new Exception(SWTarefasMessagesExceptions.RecursoNaoDisponivel);
            }
            catch (BaseCustomException ex)
            {
                context.Result = new UnauthorizedObjectResult(new UsuarioLoginResponseError(ex.Message));
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new UsuarioLoginResponseError(SWTarefasMessagesExceptions.TokenEspirado) { TokenIsExpired = true });
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new UsuarioLoginResponseError(SWTarefasMessagesExceptions.RecursoNaoDisponivel));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authentication))
                throw new CustomUnauthorizedException(SWTarefasMessagesExceptions.TokenNaoEncontrado);

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
