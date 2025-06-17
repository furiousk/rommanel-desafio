using MediatR;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Domain.Entities;
using Rommanel.Domain.Repositories;
using Rommanel.Domain.ValueObjects;

namespace Rommanel.Application.Clientes.Handlers;

public class CreateClienteHandler(
    IClienteRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly IClienteRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByDocumentoOrEmail(request.Documento, request.Email))
            throw new ApplicationException("Documento ou E-mail já cadastrado");

        var endereco = new Endereco(
            request.Cep,
            request.Logradouro,
            request.Numero,
            request.Bairro,
            request.Cidade,
            request.Estado);

        var cliente = Cliente.Create(
            request.Nome,
            request.Documento,
            request.Email,
            request.Telefone,
            request.DataNascimento,
            endereco,
            request.InscricaoEstadual,
            request.IsentoIE);

        await _repository.AddAsync(cliente);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return cliente.Id;
    }
}
