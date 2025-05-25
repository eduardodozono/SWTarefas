using Microsoft.Extensions.DependencyInjection;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Profiles;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class AddAutoMapExtension
    {
        public static IServiceCollection AddAutoMapperExtension(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
