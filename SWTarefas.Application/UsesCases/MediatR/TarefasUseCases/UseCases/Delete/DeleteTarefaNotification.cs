using MediatR;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Delete
{
    public class DeleteTarefaNotification : INotification
    {
        public Tarefa tarefa { get; set; }

        public DeleteTarefaNotification(Tarefa tarefa)
        {
            this.tarefa = tarefa;
        }
    }
}
