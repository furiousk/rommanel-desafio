using MediatR;
using Rommanel.Application.Clientes.DTOs;
using Rommanel.Application.Clientes.Queries;
using Rommanel.Domain.Repositories;

namespace Rommanel.Application.Clientes.Handlers;

public class SearchClientesHandler(IClienteRepository repository) : IRequestHandler<SearchClientesQuery, List<ClienteDto>>
{
    private readonly IClienteRepository _repository = repository;

    public async Task<List<ClienteDto>> Handle(SearchClientesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.SearchAsync(request.Nome, request.Cidade);

        return [.. result.Select(c => new ClienteDto(
            c.Id,
            c.Nome,
            c.Documento,
            c.DataNascimento,
            c.Telefone,
            c.Email,
            c.InscricaoEstadual ?? "",
            c.IsentoIE,
            c.Endereco.Cep,
            c.Endereco.Logradouro,
            c.Endereco.Numero,
            c.Endereco.Bairro,
            c.Endereco.Cidade,
            c.Endereco.Estado
        ))];
    }
}