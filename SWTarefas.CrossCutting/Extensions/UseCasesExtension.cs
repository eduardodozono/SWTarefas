using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Application.UsesCases.TarefasUseCases;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class UseCasesExtension
    {
        public static IServiceCollection AddUseCasesExtension(this IServiceCollection services)
        {
            services.AddScoped<ICreateTarefaUseCase, CreateTarefaUseCase>();
            services.AddScoped<IDeleteTarefasUseCase, DeleteTarefasUseCase>();
            services.AddScoped<IUpdateTarefasUseCase, UpdateTarefasUseCase>();
            services.AddScoped<IGetAllTarefasUseCase, GetAllTarefasUseCase>();
            services.AddScoped<IGetByIdTarefasUseCase, GetByIdTarefasUseCase>();

            return services;
        }
    }
}
