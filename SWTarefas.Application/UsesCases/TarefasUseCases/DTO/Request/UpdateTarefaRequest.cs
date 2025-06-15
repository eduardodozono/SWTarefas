using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request
{
    public class UpdateTarefaRequest : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        public UpdateTarefaRequest() { }

        public UpdateTarefaRequest(int tarefaId, string titulo, string? descricao, DateOnly? dataConclusaoPrevista, DateOnly? dataConclusaoRealizada, int status)
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
