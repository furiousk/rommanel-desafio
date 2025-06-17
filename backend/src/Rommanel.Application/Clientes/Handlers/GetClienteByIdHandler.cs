using MediatR;
using Rommanel.Application.Clientes.DTOs;
using Rommanel.Application.Clientes.Queries;
using Rommanel.Domain.Repositories;

namespace Rommanel.Application.Clientes.Handlers;

public class GetClienteByIdHandler(IClienteRepository repository) : IRequestHandler<GetClienteByIdQuery, ClienteDto>
{
    private readonly IClienteRepository _repository = repository;

    public async Task<ClienteDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException("Cliente não encontrado.");

        return new ClienteDto(
            cliente.Id,
            cliente.Nome,
            cliente.Documento,
            cliente.DataNascimento,
            cliente.Email,
            cliente.Endereco.Cidade);
    }
}