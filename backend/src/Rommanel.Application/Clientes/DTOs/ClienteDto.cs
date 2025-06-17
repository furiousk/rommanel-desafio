namespace Rommanel.Application.Clientes.DTOs;

public record ClienteDto(
    Guid Id,
    string Nome,
    string Documento,
    DateTime DataNascimento,
    string Email,
    string Cidade
);
