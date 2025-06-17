using MediatR;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Application.Clientes.Strategies.UpdateCliente;
using Rommanel.Domain.Exceptions;
using Rommanel.Domain.Repositories;

namespace Rommanel.Application.Clientes.Handlers;

public class UpdateClienteCommandHandler(
    IClienteRepository repository,
    IUnitOfWork unitOfWork,
    IEnumerable<IClienteUpdateStrategy> strategies) : IRequestHandler<UpdateClienteCommandWrapper>
{
    private readonly IClienteRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEnumerable<IClienteUpdateStrategy> _strategies = strategies;

    public async Task<Unit> Handle(UpdateClienteCommandWrapper request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Cliente não encontrado.");

        foreach (var strategy in _strategies)
        {
            strategy.Apply(cliente, request.Payload);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
