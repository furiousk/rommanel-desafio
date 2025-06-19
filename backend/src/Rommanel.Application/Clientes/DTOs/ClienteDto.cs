namespace Rommanel.Application.Clientes.DTOs;

public record ClienteDto(
    Guid Id,
    string Nome,
    string Documento,
    DateTime DataNascimento,
    string Telefone,
    string Email,
    string InscricaoEstadual,
    bool IsentoIE,
    string Cep,
    string Logradouro,
    string Numero,
    string Bairro,
    string Cidade,
    string Estado
);