using Rommanel.Domain.Entities;
using Rommanel.Domain.Repositories;
using Rommanel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Rommanel.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository, IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByDocumentoOrEmail(string documento, string email)
    {
        return await _context.Clientes.AnyAsync(c =>
            c.Documento == documento || c.Email == email);
    }
    public async Task AddAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Cliente?> GetByIdAsync(Guid id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Cliente>> SearchAsync(string? nome, string? cidade)
    {
        var query = _context.Clientes.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(c => EF.Functions.ILike(c.Nome, $"%{nome}%"));
        if (!string.IsNullOrEmpty(cidade))
            query = query.Where(c => EF.Functions.ILike(c.Endereco.Cidade, $"%{cidade}%"));

        return await query.ToListAsync();
    }
    public void Delete(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
    }
    public void Update(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
    }
}