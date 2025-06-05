using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.UseCases.Read;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.UseCases.Write;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class UseCasesExtensionSite
    {
        public static IServiceCollection AddUseCasesExtensionSite(this IServiceCollection services)
        {
            services.AddScoped<IReadTarefasUseCaseSite, ReadTarefasUseCaseSite>();
            services.AddScoped<IWriteTarefasUseCaseSite, WriteTarefasUseCaseSite>();

            return services;
        }

    }
}
