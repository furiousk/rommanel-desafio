using FluentAssertions;
using Moq;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Application.Clientes.Handlers;
using Rommanel.Domain.Entities;
using Rommanel.Domain.Exceptions;
using Rommanel.Domain.Repositories;

namespace Rommanel.UnitTests.Application.Clientes;
public class DeleteClienteCommandHandlerTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly DeleteClienteCommandHandler _handler;

    public DeleteClienteCommandHandlerTests()
    {
        _handler = new DeleteClienteCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Should_Delete_Cliente_When_Found()
    {
        var id = Guid.NewGuid();
        var cliente = Cliente.Create("Nome", "12345678900", "email@test.com", "11999999999", DateTime.Today.AddYears(-25),
            new("12345-678", "Rua X", "1", "Centro", "Cidade", "SP"), null, true);

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(cliente);

        await _handler.Handle(new DeleteClienteCommand(id), CancellationToken.None);

        _repositoryMock.Verify(r => r.Delete(cliente), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_NotFound_When_Cliente_DoesNotExist()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Cliente?)null);

        var act = async () => await _handler.Handle(new DeleteClienteCommand(id), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Cliente não encontrado.");

        _repositoryMock.Verify(r => r.Delete(It.IsAny<Cliente>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
