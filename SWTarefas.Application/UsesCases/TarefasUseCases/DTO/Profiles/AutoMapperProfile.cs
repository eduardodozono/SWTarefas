using AutoMapper;
using SWTarefas.Application.UsesCases.MediatR.DTO.Request;
using SWTarefas.Application.UsesCases.MediatR.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tarefa, CreateTarefaRequest>();
            CreateMap<CreateTarefaRequest, Tarefa>();

            CreateMap<Tarefa, CreateTarefaResponse>();
            CreateMap<CreateTarefaResponse, Tarefa>();

            CreateMap<Tarefa, UpdateTarefaResponse>();
            CreateMap<UpdateTarefaResponse, Tarefa>();

            CreateMap<Tarefa, UpdateTarefaRequest>();
            CreateMap<UpdateTarefaRequest, Tarefa>();

            CreateMap<Tarefa, GetAllTarefaResponse>();
            CreateMap<GetAllTarefaResponse, Tarefa>();

            CreateMap<Tarefa, GetByIdTarefaResponse>();
            CreateMap<GetByIdTarefaResponse, Tarefa>();

            CreateMap<Tarefa, TarefaViewModel>();
            CreateMap<TarefaViewModel, Tarefa>();

            CreateMap<FilterTarefa, FilterTarefaRequest>();
            CreateMap<FilterTarefaRequest, FilterTarefa>();

            CreateMap<Tarefa, CreateTarefaCommandRequest>();
            CreateMap<CreateTarefaCommandRequest, Tarefa>();

            CreateMap<Tarefa, CreateTarefaMResponse>();
            CreateMap<CreateTarefaMResponse, Tarefa>();            
        }
    }
}
