using MediatR;

namespace Rommanel.Application.Clientes.Commands;

public record DeleteClienteCommand(Guid Id) : IRequest;
