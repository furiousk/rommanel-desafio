using Rommanel.Domain.Entities;
using Rommanel.Domain.ValueObjects;

namespace Rommanel.UnitTests.Domain;

public class ClienteTests
{
    [Fact]
    public void Cliente_ShouldBeCreatedWithValidData()
    {
        var endereco = new Endereco("12345678", "Rua Exemplo", "123", "Centro", "Cidade", "SP");
        var cliente = new Cliente("Bruno", "12345678900", TipoPessoa.Fisica, new DateTime(1990, 1, 1), "1199999999", "bruno@email.com",
            endereco, "ISENTO", true);

        Assert.Equal("Bruno", cliente.Nome);
        Assert.Equal("12345678900", cliente.Documento);
        Assert.Equal("Cidade", cliente.Endereco.Cidade);
    }
}
