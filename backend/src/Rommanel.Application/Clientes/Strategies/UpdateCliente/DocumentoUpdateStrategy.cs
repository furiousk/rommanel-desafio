using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;

public class DocumentoUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Documento))
            cliente.SetDocument(command.Documento);
    }
}

