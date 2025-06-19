using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response
{
    public class GetAllTarefaResponse : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        public GetAllTarefaResponse(int tarefaId, string titulo, string? descricao, DateTime? dataConclusaoPrevista, DateTime? dataConclusaoRealizada, int status)
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
