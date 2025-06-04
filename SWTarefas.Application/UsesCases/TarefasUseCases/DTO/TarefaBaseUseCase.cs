namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO
{
    public abstract class TarefaBaseUseCase
    {
        public string Titulo { get; init; }
        public string? Descricao { get; init; }
        public virtual DateOnly? DataConclusaoPrevista { get; init; } = null;
        public virtual DateOnly? DataConclusaoRealizada { get; init; }
        public virtual int Status { get; init; }
    }
}
