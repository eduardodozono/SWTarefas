namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO
{
    public class UpdateTarefaRequest : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        public UpdateTarefaRequest(int tarefaId, string titulo, string? descricao, DateOnly? dataConclusaoPrevista, DateOnly? dataConclusaoRealizada, int status)
        {
            TarefaId = tarefaId;
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
            DataConclusaoRealizada = dataConclusaoRealizada;
            Status = status;
        }
    }
}
