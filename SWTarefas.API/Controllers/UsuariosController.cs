﻿using Microsoft.AspNetCore.Mvc;
using SWTarefas.API.Controllers.Bases;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.EF;

namespace SWTarefas.API.Controllers
{
    public class UsuariosController : UsuariosControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuariosLoginUseCaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomUnauthorizedException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromServices] ILoginUsuariosUseCase useCase, [FromBody] UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateUsuariosLoginUseCaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomUnauthorizedException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromServices] ICreateLoginUsuariosUseCase useCase, [FromBody] CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request, token);

            return Created(string.Empty, result);
        }
    }
}
