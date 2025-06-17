using MediatR;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Exceptions;
using Rommanel.Domain.Repositories;

namespace Rommanel.Application.Clientes.Handlers;

public class DeleteClienteCommandHandler(IClienteRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteClienteCommand>
{
    private readonly IClienteRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Cliente não encontrado.");
        _repository.Delete(cliente);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

