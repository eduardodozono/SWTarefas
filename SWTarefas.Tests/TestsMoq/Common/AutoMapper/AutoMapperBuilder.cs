using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Profiles;

namespace SWTarefas.Tests.TestsMoq.Common.AutoMapper
{
    public static class AutoMapperBuilder
    {
        public static IMapper Build()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            return configuration.CreateMapper();
        }
    }
}
