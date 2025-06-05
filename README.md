# SWTarefas (net8.0)
Projeto está dividido em:
1. SWTarefas.API<br/>
Este projeto contém as apis em rest com os endpoints para a criação, alteração, exclusão e consultas das tarefas existentes.
Para poder usar a api precisa ser criado um usuário no endpoint(/usuarios) e usar o email e senha no endpoint(/usuarios/login) para obter o token de acesso.
3. SWTarefas.Application<br/>
Este projeto contém as regras de negócio das apis e do site.
4. SWTarefas.CrossCutting<br/>
Este projeto é onde são feitas as declarações de todas as dependências que foram utilizadas na solução.
5. SWTarefas.Domain<br/>
Este projeto contém a entidade principal do projeto que é a classe de Tarefas.
6. SWTarefas.Infrastructure<br/>
Este projeto contém toda a camada de acesso a dados no caso (SQL Server).
7. SWTarefas.Resources<br/>
Este projeto contém os resources de erros da api, site e testes para validação da api.
8. SWTarefas.Site<br/>
Este projeto contém a apresentação para o usuário final das funcionalidades.
9. SWTarefas.Tests<br/>
Este projeto contém os testes da solução.
10. Heathchecks
Usar o endpoint(/health) para verificar o status da api.
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Como testar:<br/>
Para facilitar eu deixei preparado para um banco de dados em memória então para iniciar o teste é somente rodar o projeto desejado e iniciar os testes.<br/>
A parte das apis estão documentadas com o Swagger e podem ser testadas no mesmo ou em qualquer ferramenta desejada pelo endpoint ("/tarefas").
* Banco de dados persistente (Sql Server)<br/>
Nos projetos de (SWTarefas.API e SWTarefas.Site), existem os arquivos de configurações dos bancos (appsettings.json), no caso do Sql Server é preciso informar a string de conexão e mudar o tipo do banco de dados no projeto (SWTarefas.CrossCutting) dentro da pasta (Extensions) no arquivo (AddInfrastructureExtension.cs).
1. Existe uma pasta de migração do banco de dados no projeto (SWTarefas.Infrastructure)
Essa pasta contém a migração final do banco de dados.
2. Rodar o comando de migração para criar o banco e as tabelas:
update-database final
