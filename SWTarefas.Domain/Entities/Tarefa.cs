namespace SWTarefas.Domain.Entities
{
    public class Tarefa
    {
        public int TarefaId { get; init; }
        public string Titulo { get; init; }
        public string? Descricao { get; init; }
        public DateOnly? DataConclusaoPrevista { get; init; } = null;
        public DateOnly? DataConclusaoRealizada { get; init; } = null;
        public int Status { get; init; }

        public Tarefa() { }

        public Tarefa(int tarefaId, string titulo, string descricao, DateOnly dataConclusaoPrevista, DateOnly? dataConclusaoRealizada, int status)
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
