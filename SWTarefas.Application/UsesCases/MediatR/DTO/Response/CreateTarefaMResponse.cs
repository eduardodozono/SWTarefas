using System.Text.Json.Serialization;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;

namespace SWTarefas.Application.UsesCases.MediatR.DTO.Response
{
    public class CreateTarefaMResponse : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        [JsonIgnore]
        public override DateTime? DataConclusaoRealizada { get; init; } = null;

        public CreateTarefaMResponse(int tarefaId, string titulo, string? descricao, int status, DateTime? dataConclusaoPrevista)
        {
            TarefaId = tarefaId;
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataConclusaoPrevista = dataConclusaoPrevista;
        }
    }
}
