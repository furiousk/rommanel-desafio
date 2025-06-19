# 🧠 Rommanel - Desafio Full Stack

Este repositório apresenta a solução para o desafio técnico proposto pela Rommanel, com foco em arquitetura sólida, qualidade de código, design consistente e automação de pipeline.

---

## ✅ Funcionalidade

Implementação de um CRUD de cliente com os seguintes campos:

- Nome / Razão Social
- CPF / CNPJ (campo único com identificação automática do tipo de pessoa)
- Data de Nascimento / Abertura
- Telefone
- E-mail
- Inscrição Estadual ou Isento
- Endereço completo (CEP, Logradouro, Número, Bairro, Cidade, Estado)

---

## ⚙️ Tecnologias & Arquitetura

| Camada       | Tecnologias / Padrões                                       |
|--------------|--------------------------------------------------------------|
| Backend API  | ASP.NET Core 8.0, DDD, Clean Architecture                    |
| Padrões      | CQRS + MediatR, UnitOfWork, Strategy Pattern                 |
| Validações   | FluentValidation + regras no domínio                         |
| Persistência | Entity Framework Core + PostgreSQL                          |
| Testes       | xUnit, Moq, FluentAssertions                                 |
| Coverage     | Coverlet + ReportGenerator                                   |
| Frontend     | Angular 17 + Angular Material 19 (M3 Theming API)           |
| UI           | Design customizado inspirado na identidade da Rommanel      |
| Docker       | Docker Compose com pipeline de testes, cobertura e build    |

---

## 🧠 Principais decisões arquiteturais

- **DDD**: separação clara de responsabilidades com entidades ricas e coesas.
- **CQRS + MediatR**: comandos e queries desacoplados.
- **ValidationBehavior**: validações padronizadas antes dos handlers.
- **Strategy Pattern**: atualizações parciais sem `if`/`switch`.
- **Theming Angular Material 19**: tema claro e escuro com tokens M3 e estilo Rommanel.
- **Logs persistentes**: sistema de logging assíncrono com Serilog.

---

## 🚀 Como rodar o projeto com Docker

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### 1. Clonar o repositório

```bash
git clone https://github.com/furiousk/rommanel-desafio.git
cd rommanel-desafio
```

### 2. Subir os containers (backend, frontend e banco)

```bash
docker compose up --build
```

### 3. Executar as migrações do banco de dados

```bash
docker compose exec api dotnet ef database update --project Rommanel.API --startup-project Rommanel.API
```

### 4. Acessar a aplicação

- Frontend Angular: http://localhost:4200
- Backend Swagger API: http://localhost:5000/swagger
- PostgreSQL: disponível na porta 5432

---

## 🧪 Rodar testes com cobertura de código
### Para executar os testes e visualizar a cobertura de forma automática:

```bash
.\start-test-report.ps1

```

- Relatório HTML: http://localhost:5005
- Arquivo Cobertura: coverage/coverage.cobertura.xml

---

### 📂 Estrutura resumida

```text
Rommanel/
├── backend/
│   ├── src/
│   │   ├── Rommanel.API/                → Web API (.NET 8)
│   │   ├── Rommanel.Application/        → Handlers, Commands, Queries
│   │   ├── Rommanel.Domain/             → Entidades, VOs, Enums
│   │   └── Rommanel.Infrastructure/     → Repositórios, EF, Migrations
│   └── tests/
│       └── Rommanel.UnitTests/          → Testes unitários
├── frontend/
│   └── rommanel-ui/
│       └── src/
│           └── app/
│               ├── core/                → Interceptors, services globais
│               ├── shared/              → Componentes reutilizáveis (header, button, input)
│               ├── features/            → Domínio Clientes
│               │   ├── models/
│               │   ├── services/
│               │   └── pages/
│               │       ├── clientes-list/
│               │       └── clientes-form/
│               ├── app.config.ts        → Standalone bootstrap Angular 17
│               └── app.routes.ts        → Roteamento standalone
├── coverage/                            → Arquivos gerados de cobertura
├── docker-compose.yml                   → Orquestração completa
├── start-test-report.ps1                → Pipeline local (Windows)
├── start-test-report.sh                 → Pipeline local (Linux/macOS)
```

---

## ✨ Funcionalidades do Frontend

- ✅ Listagem de clientes com filtros dinâmicos reativos (nome, cidade)

- ✅ Formulário reativo com validações visuais e regras condicionais

- ✅ Máscaras para telefone e CEP com ngx-mask

- ✅ Tema visual customizado (claro/escuro) com identidade visual da marca

- ✅ Navegação por rotas standalone (app.routes.ts)

- ✅ Consumo de API REST com HttpClient e ClienteService

---

## 📄 Observações finais

- O projeto está pronto para ambientes de CI/CD.
- A camada de testes já verifica regras de negócio críticas.
- O frontend segue os padrões mais recentes do Angular 17 standalone.
- A comunicação entre camadas segue SOLID, DRY e KISS.

---

## 🔗 Autor

Desenvolvido por Bruno Diogenes Alves para o processo seletivo da Rommanel.
