using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write;
using SWTarefas.Application.UsesCases.UsuariosUseCases;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces;

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

            services.AddScoped<ILoginUsuariosUseCase, LoginUsuariosUseCase>();
            services.AddScoped<ICreateLoginUsuariosUseCase, CreateLoginUsuariosUseCase>();

            return services;
        }
    }
}
