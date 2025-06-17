using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;

public class DataNascimentoUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (command.DataNascimento.HasValue)
            cliente.SetBirthDate(command.DataNascimento.Value);
    }
}

