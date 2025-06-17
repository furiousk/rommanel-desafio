using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;
public class LogradouroUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Logradouro))
        {
            var novo = cliente.Endereco with { Logradouro = command.Logradouro };
            cliente.SetAddress(novo);
        }
    }
}
