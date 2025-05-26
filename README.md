<img src="https://swfast.com.br/wp-content/uploads/2021/10/swfast-logo.png" alt="SWFast"># SWTarefas
Projeto está dividido em:
1. SWTarefas.API
Este projeto contém as apis em rest com os endpoints para a criação, alteração, exclusão e consultas das tarefas existentes.
2. SWTarefas.Application
Este projeto contém as regras de negócio das apis e do site
3. SWTarefas.CrossCutting
Este projeto é onde são feitas as declarações de todas as dependências que foram utilizadas na solução
4. SWTarefas.Domain
Este projeto contém a entidade principal do projeto que é a classe de Tarefas.
5. SWTarefas.Infrastructure
Este projeto contém toda a camada de acesso a dados no caso (SQL Server).
6. SWTarefas.Resources
Este projeto contém os resources de erros da api, site e testes para validação da api.
7. SWTarefas.Site
Este projeto contém a apresentação para o usuário final das funcionalidades
8. SWTarefas.Tests
Este projeto contém os testes da solução.
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Como testar:
Para facilitar eu deixei preparado para um banco de dados em memória em então é somente rodar o projeto desejado e iniciar os testes.
A parte das apis estão documentadas com o Swagger e pode ser testada no mesmo ou em qualquer ferramenta desejada pelo endpoint("/tarefas").

* Banco de dados persistente (Sql Server)
Nos projetos de (SWTarefas.API e SWTarefas.Site), existem os arquivos de configurações dos bancos (appsettings.json), no caso do Sql Server é preciso informar a string de conexão.
Mudar o tipo do banco de dados no projeto (SWTarefas.CrossCutting) dentro da pasta (Extensions) no arquivo (AddInfrastructureExtension.cs), este aquivo esta comentado oque precisar ser modificado para este teste.
1. Existe uma pasta de migração do banco de dados no projeto (SWTarefas.Infrastructure)
Essa pasta contém a migração final do banco de dados.
2. Rodar o comando de migração para criar o banco e as tabelas:
update-database final
