using SWTarefas.Domain.Entities;

namespace SWTarefas.Domain.DTO.Tarefas
{
    public class TarefasUsuarios
    {
        public IEnumerable<Tarefa>? Tarefas { get; set; }
        public IEnumerable<Usuario>? Usuarios { get; set; }
    }
}
