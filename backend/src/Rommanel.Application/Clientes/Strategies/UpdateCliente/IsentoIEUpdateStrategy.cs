using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;

public class IsentoIEUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        cliente.SetIsStateRegistrationExempt(command.IsentoIE);
    }
}
