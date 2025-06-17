using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;
using Rommanel.Domain.ValueObjects;

namespace Rommanel.Application.Clientes.Strategies.UpdateCliente;

public class EnderecoUpdateStrategy : IClienteUpdateStrategy
{
    public void Apply(Cliente cliente, UpdateClienteCommand command)
    {
        var atual = cliente.Endereco;

        var novoEndereco = new Endereco(
            cep: command.Cep ?? atual.Cep,
            logradouro: command.Logradouro ?? atual.Logradouro,
            numero: command.Numero ?? atual.Numero,
            bairro: command.Bairro ?? atual.Bairro,
            cidade: command.Cidade ?? atual.Cidade,
            estado: command.Estado ?? atual.Estado
        );

        cliente.SetAddress(novoEndereco);
    }
}


