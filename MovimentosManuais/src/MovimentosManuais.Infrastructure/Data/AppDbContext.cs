using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<ProdutoCosif> ProdutosCosif => Set<ProdutoCosif>();
    public DbSet<MovimentoManual> MovimentosManuais => Set<MovimentoManual>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
