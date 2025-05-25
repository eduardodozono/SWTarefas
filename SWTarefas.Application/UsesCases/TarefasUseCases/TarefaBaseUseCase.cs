namespace SWTarefas.Application.UsesCases.TarefasUseCases
{
    public abstract class TarefaBaseUseCase
    {
        public string Titulo { get; init; }
        public string? Descricao { get; init; }
        public virtual DateOnly? DataConclusaoPrevista { get; init; }
        public virtual DateOnly? DataConclusaoRealizada { get; init; }
        public virtual int Status { get; init; }
    }
}
