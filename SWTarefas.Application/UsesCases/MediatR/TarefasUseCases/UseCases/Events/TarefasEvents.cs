using System.Diagnostics;
using MediatR;
using SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Delete;
using SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Events
{
    public class TarefasEvents : INotificationHandler<CreateTarefaNotification>, INotificationHandler<DeleteTarefaNotification>
    {
        public async Task Handle(CreateTarefaNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Tarefa event create: ({notification.tarefa.Titulo}).");
            });
        }

        public async Task Handle(DeleteTarefaNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Tarefa event deleted: ({notification.tarefa.Titulo}).");
            });
        }
    }
}
