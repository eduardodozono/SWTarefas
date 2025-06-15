using System.Text.Json.Serialization;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response
{
    public class CreateTarefaResponse : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        [JsonIgnore]
        public override DateOnly? DataConclusaoRealizada { get; init; } = null;

        public CreateTarefaResponse(int tarefaId, string titulo, string? descricao, int status, DateOnly? dataConclusaoPrevista)
        {
            TarefaId = tarefaId;
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataConclusaoPrevista = dataConclusaoPrevista;
        }
    }
}
