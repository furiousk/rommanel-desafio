using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;
public class BairroUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Bairro))
        {
            var novo = cliente.Endereco with { Bairro = command.Bairro };
            cliente.SetAddress(novo);
        }
    }
}

