using ProjetoSW.Infrastructure.DataAcess.Repository.UnitOfWork;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases;
using SWTarefas.Infrastructure.DataAcess.Repository.Tarefas;
using FluentAssertions;

namespace SWTarefas.Tests.Tests.TarefasUsesCases
{
    public class TarefasUsesCasesTests
    {
        private TarefasMockUseCases tarefasMockUseCases = new TarefasMockUseCases();
        
        [Fact]
        public async Task Execute_GetByIdTarefasUseCase_Deve_Retornar_Registro()
        {
            var tarefasContext = tarefasMockUseCases.GetDatabase(true);

            var tarefaRepository = new TarefaRepository(tarefasContext);

            var getByIdTarefasUseCase = new GetByIdTarefasUseCase(tarefaRepository, tarefasMockUseCases.GetIMapper());

            var result = await getByIdTarefasUseCase.Execute(1);

            result.Should().NotBeNull();
            result.TarefaId.Should().Be(1);
        }

        [Fact]
        public async Task Execute_GetAllTarefasUseCase_Deve_Retornar_2Registros()
        {
            var tarefasContext = tarefasMockUseCases.GetDatabase(true);

            var tarefaRepository = new TarefaRepository(tarefasContext);

            var getAllTarefasUseCase = new GetAllTarefasUseCase(tarefaRepository, tarefasMockUseCases.GetIMapper());

            var result = await getAllTarefasUseCase.Execute();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Execute_DeleteTarefasUseCase_Deve_Excluir_Registro()
        {
            var mapper = tarefasMockUseCases.GetIMapper();

            var tarefasContext = tarefasMockUseCases.GetDatabase(true);

            var tarefaRepository = new TarefaRepository(tarefasContext);

            var unitOfWork = new UnitOfWork(tarefasContext);

            var deleteTarefasUseCase = new DeleteTarefasUseCase(tarefaRepository, tarefaRepository, unitOfWork);

            var result = await deleteTarefasUseCase.Execute(1);

            result.Should().Be(1);
        }

        [Fact]
        public async Task Execute_CreateTarefaUseCase_Deve_Incluir_Reggistro()
        {
            var tarefasContext = tarefasMockUseCases.GetDatabase(false);

            var tarefaRepository = new TarefaRepository(tarefasContext);

            var unitOfWork = new UnitOfWork(tarefasContext);

            var createTarefaUseCase = new CreateTarefaUseCase(tarefaRepository, unitOfWork, tarefasMockUseCases.GetIMapper());

            var tarefaCreate = new CreateTarefaRequest
            {
                Titulo = "Titulo Create",
                Descricao = "Descricao Create",
                DataConclusaoPrevista = new DateOnly(2025, 1, 1),
                Status = 2
            };

            var result = await createTarefaUseCase.Execute(tarefaCreate);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefaCreate.Titulo);
            result.TarefaId.Should().Be(1);
        }

        [Fact]
        public async Task Execute_UpdateTarefasUseCase_Deve_Atualizar_Registro()
        {
            var tarefasContext = tarefasMockUseCases.GetDatabase(true);

            var tarefaRepository = new TarefaRepository(tarefasContext);

            var unitOfWork = new UnitOfWork(tarefasContext);

            var updateTarefasUseCase = new UpdateTarefasUseCase(tarefaRepository, tarefaRepository, unitOfWork, tarefasMockUseCases.GetIMapper());

            var tarefaUpdate = new UpdateTarefaRequest
            {
                TarefaId = 1,
                Titulo = "Titulo Update",
                Status = 1,
                DataConclusaoPrevista = new DateOnly(2025, 1, 1),
                DataConclusaoRealizada = new DateOnly(2025, 2, 2)
            };

            var result = await updateTarefasUseCase.Execute(tarefaUpdate);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefaUpdate.Titulo);
            result.TarefaId.Should().Be(1);
        }
    }
}
