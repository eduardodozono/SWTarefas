using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tarefa, CreateTarefaRequest>().ReverseMap();

            CreateMap<Tarefa, CreateTarefaResponse>().ReverseMap();

            CreateMap<Tarefa, UpdateTarefaResponse>().ReverseMap();

            CreateMap<Tarefa, UpdateTarefaRequest>().ReverseMap();

            CreateMap<UpdateTarefaRequest, Tarefa>().ReverseMap();

            CreateMap<Tarefa, GetAllTarefaResponse>().ReverseMap();

            CreateMap<Tarefa, GetByIdTarefaResponse>().ReverseMap();
            CreateMap<GetByIdTarefaResponse, Tarefa>().ReverseMap();

            CreateMap<Tarefa, TarefaViewModel>().ReverseMap();
            CreateMap<TarefaViewModel, Tarefa>().ReverseMap();
        }
    }
}
