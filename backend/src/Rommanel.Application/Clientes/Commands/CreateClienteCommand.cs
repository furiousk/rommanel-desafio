using MediatR;

namespace Rommanel.Application.Clientes.Commands;

public record CreateClienteCommand(
    string Nome,
    string Documento,
    DateTime DataNascimento,
    string Telefone,
    string Email,
    string Cep,
    string Logradouro,
    string Numero,
    string Bairro,
    string Cidade,
    string Estado,
    string? InscricaoEstadual,
    bool IsentoIE
) : IRequest<Guid>;