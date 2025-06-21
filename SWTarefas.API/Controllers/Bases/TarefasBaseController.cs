using Microsoft.AspNetCore.Mvc;
using SWTarefas.API.Attribute;

namespace SWTarefas.API.Controllers.Bases
{
    [Route("[controller]")]
    [ApiController]
    [AuthenticatedUser]
    public class TarefasBaseController : ControllerBase { }
}
