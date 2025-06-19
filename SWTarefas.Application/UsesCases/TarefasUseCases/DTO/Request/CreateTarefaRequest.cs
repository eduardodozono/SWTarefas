using System.Text.Json.Serialization;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request
{
    public class CreateTarefaRequest : TarefaBaseUseCase
    {
        [JsonIgnore]
        public override int Status { get; init; } = (int)TarefaStatus.Pendente;

        [JsonIgnore]
        public override DateTime? DataConclusaoRealizada { get; init; } = null;

        public CreateTarefaRequest() { }

        public CreateTarefaRequest(string titulo, string? descricao, DateTime? dataConclusaoPrevista)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
        }
    }
}
