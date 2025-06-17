using Microsoft.EntityFrameworkCore;
using Rommanel.Domain.Entities;

namespace Rommanel.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.OwnsOne(c => c.Endereco);
        });

        base.OnModelCreating(modelBuilder);
    }
}
