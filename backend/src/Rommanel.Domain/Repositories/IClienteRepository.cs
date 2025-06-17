using Rommanel.Domain.Entities;

namespace Rommanel.Domain.Repositories;

public interface IClienteRepository
{
    Task<bool> ExistsByDocumentoOrEmail(string documento, string email);
    Task AddAsync(Cliente cliente);
    Task<Cliente?> GetByIdAsync(Guid id);
    Task<List<Cliente>> SearchAsync(string? nome, string? cidade);
    void Delete(Cliente cliente);
    void Update(Cliente cliente);
}
