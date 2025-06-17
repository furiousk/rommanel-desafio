using Moq;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Application.Clientes.Handlers;
using Rommanel.Application.Clientes.Validators;
using Rommanel.Domain.Repositories;

namespace Rommanel.UnitTests.Application.Clientes;

public class CreateClienteHandlerTests
{
    [Fact]
    public async Task CreateClienteHandler_ShouldReturnIdWhenValid()
    {
        var fakeRepo = new Mock<IClienteRepository>();
        fakeRepo.Setup(r => r.ExistsByDocumentoOrEmail(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

        var fakeUow = new Mock<IUnitOfWork>();

        var handler = new CreateClienteHandler(fakeRepo.Object, fakeUow.Object);
        var command = new CreateClienteCommand("Bruno", "12345678900", new DateTime(1990, 1, 1), "1199999999", "bruno@email.com",
            "12345678", "Rua Exemplo", "123", "Centro", "São Paulo", "SP", "", true);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
    }
    [Fact]
    public void CreateClienteHandler_ShouldFail_WhenCorporateWithoutIEAndNotExempt()
    {
        var validator = new CreateClienteValidator();

        var command = new CreateClienteCommand("Empresa X", "12345678000100",
            new DateTime(2000, 1, 1), "1199999999", "empresa@email.com",
            "12345678", "Av. Empresa", "100", "Centro", "São Paulo", "SP", "", false);

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "InscricaoEstadual" && e.ErrorMessage.Contains("obrigatória"));
    }
    [Fact]
    public async Task CreateClienteHandler_ShouldAllowEmptyIE_WhenExempt()
    {
        var fakeRepo = new Mock<IClienteRepository>();
        fakeRepo.Setup(r => r.ExistsByDocumentoOrEmail(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

        var fakeUow = new Mock<IUnitOfWork>();

        var handler = new CreateClienteHandler(fakeRepo.Object, fakeUow.Object);

        var command = new CreateClienteCommand("Empresa Y", "12345678000101", new DateTime(2000, 1, 1), "1199999999", "empresa2@email.com",
            "12345678", "Av. Teste", "200", "Centro", "São Paulo", "SP", "", true);

        var result = await handler.Handle(command, CancellationToken.None);
        Assert.NotEqual(Guid.Empty, result);
    }
    [Fact]
    public void CreateClienteHandler_ShouldFail_WhenUnderage()
    {
        var validator = new CreateClienteValidator();
        var command = new CreateClienteCommand("Bruno", "12345678900", DateTime.Today.AddYears(-17), "1199999999", "bruno@email.com",
            "12345678", "Rua Exemplo", "123", "Centro", "São Paulo", "SP", "", true);

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "DataNascimento" &&
            e.ErrorMessage == "Cliente deve ser maior de 18 anos.");
    }
    [Fact]
    public async Task CreateClienteHandler_ShouldFail_WhenEmailOrDocumentoExists()
    {
        var fakeRepo = new Mock<IClienteRepository>();
        fakeRepo.Setup(r => r.ExistsByDocumentoOrEmail(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

        var fakeUow = new Mock<IUnitOfWork>();

        var handler = new CreateClienteHandler(fakeRepo.Object, fakeUow.Object);

        var command = new CreateClienteCommand(
            "Duplicado", "12345678900", new DateTime(1990, 1, 1),
            "1199999999", "duplicado@email.com",
            "12345678", "Rua", "123", "Bairro", "Cidade", "SP", "ISENTO", true
        );

        var ex = await Record.ExceptionAsync(() => handler.Handle(command, CancellationToken.None));

        Assert.NotNull(ex);
        Assert.Contains("documento ou e-mail", ex!.Message, StringComparison.OrdinalIgnoreCase);
    }
}
