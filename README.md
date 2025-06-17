# 🧠 Rommanel - Desafio Full Stack

Este repositório apresenta a solução para o desafio técnico proposto pela Rommanel, com foco em arquitetura sólida, qualidade de código e automação de pipeline.

---

## ✅ Funcionalidade

Implementação de um CRUD de cliente com os seguintes campos:

- Nome / Razão Social
- CPF / CNPJ (campo único com identificação automática do tipo de pessoa)
- Data de Nascimento
- Telefone
- E-mail
- Inscrição Estadual ou Isento
- Endereço completo (CEP, Logradouro, Número, Bairro, Cidade, Estado)

---

## ⚙️ Tecnologias & Arquitetura

| Camada       | Tecnologias / Padrões                                      |
|--------------|-------------------------------------------------------------|
| Backend API  | ASP.NET Core 8.0, DDD, Clean Architecture                   |
| Padrões      | CQRS + MediatR, UnitOfWork, Strategy Pattern                |
| Validações   | FluentValidation                                            |
| Persistência | Entity Framework Core + PostgreSQL                         |
| Testes       | xUnit, Moq, FluentAssertions                                |
| Coverage     | Coverlet + ReportGenerator                                  |
| Docker       | Docker Compose com pipeline de testes + build + cobertura  |

---

## 🧠 Principais decisões arquiteturais

- **Domain-Driven Design (DDD)**: separação clara de responsabilidades com entidades ricas e comportamentos encapsulados.
- **CQRS + MediatR**: divisão entre comandos e queries, usando `Handlers` e `Requests` desacoplados.
- **UnitOfWork**: controle transacional explícito, aplicando o padrão em conjunto com os repositórios.
- **Middleware de exceções**: captura e formatação centralizada de erros, com suporte a mensagens personalizadas.
- **Atualização parcial com Strategy Pattern**: cada campo é tratado por uma strategy separada, eliminando `ifs` e tornando a lógica extensível.
- **Value Objects imutáveis**: `Endereco` implementado como `record`, com suporte a cópia (`with`) e validações de construção.

---

## 🚀 Como rodar o projeto com Docker

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PowerShell (Windows)] ou [bash (Linux/macOS)]

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/rommanel-desafio.git
cd rommanel-desafio
```

### 2. Rodar os containers

```powershell
.\start-test-report.ps1
```

"""Este script:

- Roda os testes unitários
- Gera a cobertura de testes (`coverage.cobertura.xml`)
- Constrói o relatório HTML
- Sobe o container nginx para visualização
- Abre o navegador automaticamente

---

### 📊 Relatório de cobertura de testes

Após rodar o script, acesse:

```text
http://localhost:5005
```

Você verá a interface HTML com o relatório gerado automaticamente com `coverlet.console` + `reportgenerator`.

---

### 📂 Estrutura resumida

```text
Rommanel/
├── backend/
│   ├── src/                  → projetos principais (.API, .Domain, .Infrastructure, .Application)
│   └── tests/                → testes unitários (xUnit)
├── frontend/                 → frontend Angular (não incluso neste escopo)
├── tools/reportgenerator/    → imagem personalizada para gerar HTML
├── coverage/                 → saída da cobertura de testes
├── docker-compose.yml        → orquestração completa
├── start-test-report.ps1     → pipeline local
```

---

### 📄 Observações finais

- O projeto está pronto para ser executado em ambientes de CI/CD.
- A arquitetura está preparada para crescimento com eventos de domínio, integração com frontend Angular e testes de integração futuros.
- A cobertura de testes é medida automaticamente e pode ser inspecionada visualmente.
- Todas as validações são feitas em duas camadas: `FluentValidation` e regras de negócio no domínio (`Cliente`).

---

### 🔗 Autor

Desenvolvido por [Seu Nome] para o processo seletivo da Rommanel.
