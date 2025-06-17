using FluentAssertions;
using Moq;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Application.Clientes.Handlers;
using Rommanel.Application.Clientes.Strategies.UpdateCliente;
using Rommanel.Domain.Entities;
using Rommanel.Domain.Exceptions;
using Rommanel.Domain.Repositories;

namespace Rommanel.UnitTests.Application.Clientes
{
    public class UpdateClienteCommandHandlerTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly List<Mock<IClienteUpdateStrategy>> _strategyMocks = new();
        private readonly UpdateClienteCommandHandler _handler;

        public UpdateClienteCommandHandlerTests()
        {
            var nomeStrategy = new Mock<IClienteUpdateStrategy>();
            var emailStrategy = new Mock<IClienteUpdateStrategy>();

            _strategyMocks.Add(nomeStrategy);
            _strategyMocks.Add(emailStrategy);

            _handler = new UpdateClienteCommandHandler(
                _repositoryMock.Object,
                _unitOfWorkMock.Object,
                [nomeStrategy.Object, emailStrategy.Object]
            );
        }

        [Fact]
        public async Task Should_Apply_Strategies_And_Commit()
        {
            var id = Guid.NewGuid();
            var cliente = Cliente.Create("Nome", "12345678900", "email@test.com", "11999999999", DateTime.Today.AddYears(-25),
                new("12345-678", "Rua X", "1", "Centro", "Cidade", "SP"), null, true);

            var command = new UpdateClienteCommand
            {
                Nome = "Atualizado",
                Email = "novo@email.com"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(cliente);

            await _handler.Handle(new UpdateClienteCommandWrapper(id, command), CancellationToken.None);

            foreach (var strategy in _strategyMocks)
            {
                strategy.Verify(s => s.Apply(cliente, command), Times.Once);
            }
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Cliente_NotFound()
        {
            var id = Guid.NewGuid();
            var command = new UpdateClienteCommand { Nome = "Novo" };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Cliente?)null);

            var act = async () => await _handler.Handle(new UpdateClienteCommandWrapper(id, command), CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();

            foreach (var strategy in _strategyMocks)
            {
                strategy.Verify(s => s.Apply(It.IsAny<Cliente>(), It.IsAny<UpdateClienteCommand>()), Times.Never);
            }
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
