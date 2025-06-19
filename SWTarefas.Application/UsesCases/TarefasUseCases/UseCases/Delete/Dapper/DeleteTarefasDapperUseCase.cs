using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.Dapper;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete.Dapper
{
    public class DeleteTarefasDapperUseCase: IDeleteTarefasDapperUseCase
    {
        private readonly ITarefaDeleteDapperRepository _tarefaDeleteDapperRepository;
        private readonly ITarefaReadDapperRepository _tarefaReadDapperRepository;
        private readonly IUnitOfWorkDapper _unitOfWorkDapper;

        public DeleteTarefasDapperUseCase(ITarefaDeleteDapperRepository tarefaDeleteDapperRepository, ITarefaReadDapperRepository tarefaReadDapperRepository, IUnitOfWorkDapper unitOfWorkDapper)
        {
            _tarefaDeleteDapperRepository = tarefaDeleteDapperRepository;
            _tarefaReadDapperRepository = tarefaReadDapperRepository;
            _unitOfWorkDapper = unitOfWorkDapper;
        }

        public async Task<int> Execute(int tarefaId, CancellationToken token = default)
        {
            var tarefaDB = await _tarefaReadDapperRepository.GetById(tarefaId);

            if (tarefaDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            _unitOfWorkDapper.BeginTransaction();

            await _tarefaDeleteDapperRepository.Delete(tarefaId, token);

            _unitOfWorkDapper.Commit();

            return tarefaId;
        }
    }
}
