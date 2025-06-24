using System.Diagnostics;
using Hangfire;
using MediatR;
using SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Delete;
using SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write.Create;
using SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write.Update;
using SWTarefas.Jobs.Jobs.Tarefas;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Events
{
    public class TarefasEvents : INotificationHandler<CreateTarefaNotification>,
        INotificationHandler<DeleteTarefaNotification>,
        INotificationHandler<UpdateTarefaNotification>
    {
        public async Task Handle(CreateTarefaNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Tarefa event create: ({notification.tarefa.Titulo}).");

                BackgroundJob.Enqueue(() => TarefasNotificacaoJob.Created(notification.tarefa));
            });
        }

        public async Task Handle(DeleteTarefaNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Tarefa event deleted: ({notification.tarefa.Titulo}).");
            });
        }

        public async Task Handle(UpdateTarefaNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Tarefa event updated: ({notification.tarefa.Titulo}).");
            });
        }
    }
}
