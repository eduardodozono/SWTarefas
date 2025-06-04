using Microsoft.AspNetCore.Mvc;
using SWTarefas.API.Filters;

namespace SWTarefas.API.Attribute
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
