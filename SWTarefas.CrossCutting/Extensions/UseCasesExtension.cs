using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read.EF;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.EF;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class UseCasesExtension
    {
        public static IServiceCollection AddUseCasesExtension(this IServiceCollection services)
        {
            //EF Tarefas
            services.AddScoped<ICreateTarefaUseCase, CreateTarefaUseCase>();
            services.AddScoped<IUpdateTarefasUseCase, UpdateTarefasUseCase>();
            services.AddScoped<IDeleteTarefasUseCase, DeleteTarefasUseCase>();            
            services.AddScoped<IGetAllTarefasUseCase, GetAllTarefasUseCase>();
            services.AddScoped<IGetByIdTarefasUseCase, GetByIdTarefasUseCase>();
            services.AddScoped<IFilterTarefasUseCase, FilterTarefasUseCase>();

            // EF Usuario
            services.AddScoped<ILoginUsuariosUseCase, LoginUsuariosUseCase>();
            services.AddScoped<ICreateLoginUsuariosUseCase, CreateLoginUsuariosUseCase>();

            // Dapper Tarefas
            services.AddScoped<ICreateTarefaDapperUseCase, CreateTarefaDapperUseCase>();
            services.AddScoped<IUpdateTarefasDapperUseCase, UpdateTarefasDapperUseCase>();
            services.AddScoped<IDeleteTarefasDapperUseCase, DeleteTarefasDapperUseCase>();
            services.AddScoped<IGetAllTarefasDapperUseCase, GetAllTarefasDapperUseCase>();
            services.AddScoped<IGetByIdTarefasDapperUseCase, GetByIdTarefasDapperUseCase>();
            services.AddScoped<IFilterTarefasDapperUseCase, FilterTarefasDapperUseCase>();

            // Dapper Usuarios
            services.AddScoped<ILoginUsuariosDapperUseCase, LoginUsuariosDapperUseCase>();
            services.AddScoped<ICreateLoginUsuariosDapperUseCase, CreateLoginUsuariosDapperUseCase>();            

            return services;
        }
    }
}
