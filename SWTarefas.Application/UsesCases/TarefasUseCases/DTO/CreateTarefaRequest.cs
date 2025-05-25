using System.Text.Json.Serialization;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO
{
    public class CreateTarefaRequest : TarefaBaseUseCase
    {
        [JsonIgnore]
        public override int Status { get; init; } = (int)TarefaStatus.Pendente;

        [JsonIgnore]
        public override DateOnly? DataConclusaoRealizada { get; init; } = null;

        public CreateTarefaRequest(string titulo, string? descricao, DateOnly? dataConclusaoPrevista)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
        }
    }
}
