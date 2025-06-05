using SWTarefas.Domain.Entities;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using SWTarefas.Tests.TestsMoq.Common.Token;
using System.Net.Http.Headers;
using System.Net.Http;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;

namespace SWTarefas.Tests.TestsMoq.Common.UtilsApi
{
    public static class UtilsApi
    {
        public static async Task<List<Usuario>> Start_UsuarioList_Encrypt(CustomWebApplicationFactory _factory, string senha)
        {
            await TarefasDataBaseUtils.DeleteAllUsuarios(_factory);

            var listaUsuarios = UsuariosListBuilder.Build_Encrypt(senha);

            await TarefasDataBaseUtils.CreateUsuarios(_factory, listaUsuarios);

            return listaUsuarios;
        }

        public static async Task<List<Usuario>> Start_UsuarioList(CustomWebApplicationFactory _factory)
        {
            await TarefasDataBaseUtils.DeleteAllUsuarios(_factory);

            var listaUsuarios = UsuariosListBuilder.Build();

            await TarefasDataBaseUtils.CreateUsuarios(_factory, listaUsuarios);

            return listaUsuarios;
        }

        public static async Task<List<Tarefa>> Start_TarefasList(CustomWebApplicationFactory _factory)
        {
            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            var listaTarefas = TarefasListBuilder.Build_Create();

            await TarefasDataBaseUtils.CreateTarefas(_factory, listaTarefas);

            return listaTarefas;
        }

        public static void AddTokenHeader(HttpClient _httpClient, Usuario usuario)
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(usuario.UsuarioIdentifier);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
