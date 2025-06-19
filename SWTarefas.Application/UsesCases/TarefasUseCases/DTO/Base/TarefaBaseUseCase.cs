namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Base
{
    public abstract class TarefaBaseUseCase
    {
        public string Titulo { get; init; }
        public string? Descricao { get; init; }
        public virtual DateTime? DataConclusaoPrevista { get; init; } = null;
        public virtual DateTime? DataConclusaoRealizada { get; init; }
        public virtual int Status { get; init; }
    }
}
