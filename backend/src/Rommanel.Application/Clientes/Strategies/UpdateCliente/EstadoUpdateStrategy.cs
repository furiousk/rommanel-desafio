using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;
public class EstadoUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Estado))
        {
            var novo = cliente.Endereco with { Estado = command.Estado };
            cliente.SetAddress(novo);
        }
    }
}
