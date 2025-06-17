using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente
{
    public class NumeroUpdateStrategy : IClienteUpdateStrategy
    {
        public void Apply(Cliente cliente, UpdateClienteCommand command)
        {
            if (!string.IsNullOrWhiteSpace(command.Numero))
            {
                var novo = cliente.Endereco with { Numero = command.Numero };
                cliente.SetAddress(novo);
            }
        }
    }
}
