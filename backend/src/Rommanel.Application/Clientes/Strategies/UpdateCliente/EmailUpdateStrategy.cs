using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;
public class EmailUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Email))
            cliente.SetEmail(command.Email);
    }
}
