using System.Text.Json.Serialization;
using MediatR;
using SWTarefas.Application.UsesCases.MediatR.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;

namespace SWTarefas.Application.UsesCases.MediatR.DTO.Request
{
    public class CreateTarefaCommandRequest : TarefaBaseUseCase, IRequest<CreateTarefaMResponse>
    {
        [JsonIgnore]
        public override int Status { get; init; } = (int)TarefaStatus.Pendente;

        [JsonIgnore]
        public override DateTime? DataConclusaoRealizada { get; init; } = null;

        public CreateTarefaCommandRequest() { }

        public CreateTarefaCommandRequest(string titulo, string? descricao, DateTime? dataConclusaoPrevista)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
        }
    }
}
