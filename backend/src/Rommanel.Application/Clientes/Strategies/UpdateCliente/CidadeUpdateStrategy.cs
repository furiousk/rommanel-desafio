using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;
public class CidadeUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Cidade))
        {
            var novo = cliente.Endereco with { Cidade = command.Cidade };
            cliente.SetAddress(novo);
        }
    }
}
