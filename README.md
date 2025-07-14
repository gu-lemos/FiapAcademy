# FiapAcademyAdmin

Sistema de gerenciamento de alunos e turmas da FIAP Academy.

## Tecnologias Utilizadas

O projeto utiliza as seguintes tecnologias principais:

- **.NET 9.0**: Plataforma principal para desenvolvimento backend e frontend (Blazor).
- **ASP.NET Core Web API**: Para construção das APIs RESTful.
- **Blazor Server**: Para a interface web interativa e moderna.
- **Entity Framework Core (InMemory)**: Persistência de dados em memória para facilitar testes e desenvolvimento.
- **MediatR**: Implementação do padrão Mediator para desacoplamento entre camadas e handlers.
- **AutoMapper**: Mapeamento automático entre entidades e DTOs.
- **FluentValidation**: Validação de dados de entrada de forma fluente e desacoplada.
- **JWT (JSON Web Token)**: Autenticação e autorização segura via tokens.
- **Swagger (Swashbuckle)**: Documentação interativa das APIs.
- **Bootstrap 5**: Framework CSS para responsividade e design moderno.
- **xUnit, Moq, Bogus, FluentAssertions, AutoFixture**: Ferramentas para testes automatizados e geração de dados fake.

Cada tecnologia foi escolhida para garantir robustez, escalabilidade, segurança e facilidade de manutenção do sistema.

## Funcionalidades Principais

- **Autenticação e Registro de Usuários**: Cadastro, login e logout com autenticação JWT.
- **Gerenciamento de Alunos**: Cadastro, edição, exclusão e consulta de alunos.
- **Gerenciamento de Turmas**: Criação, edição, exclusão e consulta de turmas.
- **Matrícula de Alunos em Turmas**: Adição e remoção de alunos em turmas.
- **Busca Rápida**: Pesquisa eficiente de alunos e turmas.
- **Interface Responsiva**: Layout moderno e adaptável a dispositivos móveis.
- **Documentação de API**: Swagger UI para explorar e testar endpoints.
- **Validação de Dados**: Validações robustas com mensagens claras para o usuário.
- **Segurança**: Proteção de rotas e dados sensíveis via autenticação JWT.
- **Testes Automatizados**: Cobertura de testes para garantir a qualidade do sistema.

## Entidades Principais

- **Aluno**: Representa um estudante, com dados como nome, data de nascimento, CPF, email e senha.
- **Turma**: Representa uma turma, com nome, descrição e lista de alunos matriculados.
- **Matrícula**: Relaciona um aluno a uma turma, registrando a data da matrícula.
- **Usuário**: Responsável pelo acesso administrativo ao sistema, com autenticação e autorização via JWT.

## Arquitetura do Sistema

O sistema segue o padrão **Clean Architecture**, dividido em camadas bem definidas:

- **Domain**: Contém as entidades de negócio e regras fundamentais.
- **Application**: Camada de aplicação, com serviços, handlers, DTOs, validações e uso do MediatR para orquestração.
- **Infrastructure**: Implementação de repositórios, contexto de dados (Entity Framework InMemory) e integrações externas.
- **API**: Exposição de endpoints RESTful, autenticação JWT, documentação Swagger e integração com as camadas internas.
- **Web**: Interface Blazor Server, consumindo a API e apresentando uma experiência moderna e responsiva ao usuário.

Principais padrões e práticas:
- **MediatR** para handlers de comandos e queries (CQRS).
- **AutoMapper** para mapeamento entre entidades e DTOs.
- **FluentValidation** para validação de dados.
- **Injeção de Dependência** em todas as camadas.
- **Testes Automatizados** cobrindo regras de negócio e integrações.

## Como Rodar o Sistema Localmente

1. **Pré-requisitos:**
   - .NET 9.0 SDK instalado
   - Visual Studio 2022+ ou VS Code

2. **Clone o repositório:**
   ```bash
   git clone https://github.com/gu-lemos/FiapAcademy.git
   cd FiapAcademyAdmin
   ```

3. **Rode a API:**
   ```bash
   cd src/FiapAcademyAdmin.API
   dotnet run
   ```
   Acesse a documentação Swagger em: http://localhost:5114/swagger

4. **Rode o Frontend Web:**
   ```bash
   cd src/FiapAcademyAdmin.Web
   dotnet run
   ```
   Acesse a interface em: http://localhost:5000

5. **Testes Automatizados:**
   ```bash
   cd src/FiapAcademyAdmin.Tests
   dotnet test
   ```
