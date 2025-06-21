using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;

[assembly: InternalsVisibleTo("SWTarefas.API")]
namespace SWTarefas.Workers.Workers.Tarefas
{
    internal class TarefasDeleteVencidosWorker : BackgroundService
    {
        private readonly ILogger<TarefasDeleteVencidosWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public TarefasDeleteVencidosWorker(ILogger<TarefasDeleteVencidosWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timeInMinutes = int.TryParse(_configuration.GetSection("Settings:Workers:TimeInMinutesToRun").Value, out var time) ? time : 10;

            _logger.LogInformation("Iniciando o serviço");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Serviço em execução em: {DateTimeOffset.Now}");

                using var scope = _serviceProvider.CreateScope();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var tarefaReadDapperRepository = scope.ServiceProvider.GetRequiredService<ITarefaReadRepository>();
                var tarefaDeleteDapperRepository = scope.ServiceProvider.GetRequiredService<ITarefaDeleteRepository>();

                var filterTarefa = new FilterTarefa()
                {
                    DataConclusaoPrevistaInferior = DateTime.Now
                };

                var listaTarefasVencidas = await tarefaReadDapperRepository.Filter(filterTarefa);

                if (listaTarefasVencidas?.Any() ?? false)
                {
                    foreach (var tarefa in listaTarefasVencidas)
                    {
                        await tarefaDeleteDapperRepository.Delete(tarefa.TarefaId, stoppingToken);

                        await unitOfWork.Commit();
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(timeInMinutes), stoppingToken);
            }

            _logger.LogInformation($"Parando o serviço em: {DateTimeOffset.Now}");
        }
    }
}
