using MediatR;

namespace SWTarefas.Application.UsesCases.MediatR.DTO.Request
{
    public class DeleteTarefaCommandRequest: IRequest<int>
    {
        public int TarefaId { get; set; }
    }
}
