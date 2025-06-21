using Microsoft.AspNetCore.SignalR.Client;
using SWTarefas.Domain.Entities;

const string urlSignalR = "https://localhost:7117/tarefasSR";


await using var connection = new HubConnectionBuilder()
    .WithUrl(urlSignalR).Build();

await connection.StartAsync();

await foreach (var tarefa in connection.StreamAsync<Tarefa>("ListaTarefasStreaming"))
{
    Console.WriteLine($"Titulo: {tarefa.Titulo} - Descricao: {tarefa.Descricao}");
}
