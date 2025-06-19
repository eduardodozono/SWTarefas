namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request
{
    public class FilterTarefaRequest
    {
        public int TarefaId { get; init; }
        public string? Titulo { get; init; } = string.Empty;
        public string? Descricao { get; init; } = string.Empty;
        public DateTime? DataConclusaoPrevista { get; init; } = null;
        public DateTime? DataConclusaoRealizada { get; init; } = null;
        public int Status { get; init; }
    }
}
