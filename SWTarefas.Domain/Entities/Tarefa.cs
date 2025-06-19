namespace SWTarefas.Domain.Entities
{
    public class Tarefa
    {
        public int TarefaId { get; init; }
        public string Titulo { get; init; } = string.Empty;
        public string? Descricao { get; init; } = string.Empty;
        public DateTime? DataConclusaoPrevista { get; init; } = null;
        public DateTime? DataConclusaoRealizada { get; init; } = null;
        public int Status { get; init; }

        public Tarefa() { }

        public Tarefa(int tarefaId, string titulo, string descricao, DateTime dataConclusaoPrevista, DateTime? dataConclusaoRealizada, int status)
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
