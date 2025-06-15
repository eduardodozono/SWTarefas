using AutoMapper;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read;
using SWTarefas.Application.UsesCases.TarefasUseCases.Validations;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read
{
    public class FilterTarefasUseCase : IFilterTarefasUseCase
    {
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IMapper _mapper;

        public FilterTarefasUseCase(ITarefaReadRepository tarefaReadRepository, IMapper mapper)
        {
            _tarefaReadRepository = tarefaReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTarefaResponse>?> Execute(FilterTarefaRequest request, CancellationToken token = default)
        {
            await Validate(request);

            var filterDTO = _mapper.Map<FilterTarefa>(request);

            var resultDomain = await _tarefaReadRepository.Filter(filterDTO, token);

            var resultResponse = _mapper.Map<IEnumerable<GetAllTarefaResponse>?>(resultDomain);

            return resultResponse;
        }

        public static async Task Validate(FilterTarefaRequest request)
        {
            var validator = new FilterTarefasValidator();

            var resultValidator = await validator.ValidateAsync(request);

            if (!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.Select(x=> x.ErrorMessage).Distinct().ToList();

                throw new CustomBadRequestException(errors);
            }
        }
    }
}
