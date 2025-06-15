namespace SWTarefas.Domain.DTO.Tarefas
{
    public class FilterTarefa
    {
        public int TarefaId { get; init; }
        public string? Titulo { get; init; } = string.Empty;
        public string? Descricao { get; init; } = string.Empty;
        public DateOnly? DataConclusaoPrevista { get; init; } = null;
        public DateOnly? DataConclusaoRealizada { get; init; } = null;
        public int Status { get; init; }
    }
}
