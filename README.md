# SWTarefas
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
Para facilitar eu deixei preparado para um banco de dados em memória em então é somente rodar o projeto desejado (SWTarefas.API, SWTarefas.Site) e iniciar os testes.
A parte das apis estão documentadas com o Swagger e pode ser testada no mesmo.
