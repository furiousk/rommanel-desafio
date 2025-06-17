using MediatR;

namespace Rommanel.Application.Clientes.Commands;

public class UpdateClienteCommandWrapper(Guid id, UpdateClienteCommand payload) : IRequest
{
    public Guid Id { get; } = id;
    public UpdateClienteCommand Payload { get; } = payload;
}
