using MediatR;
using Rommanel.Application.Clientes.DTOs;

namespace Rommanel.Application.Clientes.Queries;

public record SearchClientesQuery(string? Nome, string? Cidade) : IRequest<List<ClienteDto>>;