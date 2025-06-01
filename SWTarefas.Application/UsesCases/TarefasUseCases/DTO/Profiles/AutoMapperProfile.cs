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
            CreateMap<CreateTarefaRequest, Tarefa>().ReverseMap();

            CreateMap<Tarefa, CreateTarefaResponse>().ReverseMap();
            CreateMap<CreateTarefaResponse, Tarefa>().ReverseMap();

            CreateMap<Tarefa, UpdateTarefaResponse>().ReverseMap();
            CreateMap<UpdateTarefaResponse, Tarefa>().ReverseMap();

            CreateMap<Tarefa, UpdateTarefaRequest>().ReverseMap();
            CreateMap<UpdateTarefaRequest, Tarefa>().ReverseMap();

            CreateMap<Tarefa, GetAllTarefaResponse>().ReverseMap();
            CreateMap<GetAllTarefaResponse, Tarefa>().ReverseMap();

            CreateMap<Tarefa, GetByIdTarefaResponse>().ReverseMap();
            CreateMap<GetByIdTarefaResponse, Tarefa>().ReverseMap();

            CreateMap<Tarefa, TarefaViewModel>().ReverseMap();
            CreateMap<TarefaViewModel, Tarefa>().ReverseMap();                       
        }
    }
}
