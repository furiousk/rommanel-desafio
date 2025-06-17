using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;

public interface IClienteUpdateStrategy
{
    void Apply(Cliente cliente, UpdateClienteCommand command);
}
