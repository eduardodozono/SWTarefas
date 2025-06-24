using MediatR;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.MediatR.DTO.Request
{
    public class UpdateTarefaCommandRequest : UpdateTarefaRequest, IRequest<UpdateTarefaResponse>
    {
        public UpdateTarefaCommandRequest() { }

        public UpdateTarefaCommandRequest(int tarefaId, string titulo, string? descricao, DateTime? dataConclusaoPrevista, DateTime? dataConclusaoRealizada, int status)
        {
            TarefaId = tarefaId;
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
            DataConclusaoRealizada = dataConclusaoRealizada;
            Status = status;
        }
    }
}
