using MediatR;
using Rommanel.Application.Clientes.DTOs;

namespace Rommanel.Application.Clientes.Queries;

public record GetClienteByIdQuery(Guid Id) : IRequest<ClienteDto>;
