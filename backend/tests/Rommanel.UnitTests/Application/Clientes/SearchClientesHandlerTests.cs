using Moq;
using Rommanel.Application.Clientes.Handlers;
using Rommanel.Application.Clientes.Queries;
using Rommanel.Domain.Entities;
using Rommanel.Domain.Repositories;
using Rommanel.Domain.ValueObjects;

namespace Rommanel.UnitTests.Application.Clientes;

public class SearchClientesHandlerTests
{
    [Fact]
    public async Task SearchClientesHandler_ShouldFilterByName()
    {
        var clientes = new List<Cliente> 
        {
            new (
                "Bruno",
                "000.000.000-27",
                TipoPessoa.Fisica,
                DateTime.Now,
                "21999999999",
                "bruno@gmail.com",
                new Endereco("1","r","1","b","c","e"),
                "",
                true
            )
        };

        var fakeRepo = new Mock<IClienteRepository>();
        fakeRepo.Setup(r => r.SearchAsync("bru", null)).ReturnsAsync(clientes);

        var handler = new SearchClientesHandler(fakeRepo.Object);
        var result = await handler.Handle(new SearchClientesQuery("bru", null), CancellationToken.None);

        Assert.Single(result);
        Assert.Equal("Bruno", result[0].Nome);
    }
}