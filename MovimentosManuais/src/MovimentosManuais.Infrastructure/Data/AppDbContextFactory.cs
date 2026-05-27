using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovimentosManuais.Infrastructure.Data;

public sealed class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__MovimentosManuaisDb")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings:MovimentosManuaisDb")
            ?? "Server=127.0.0.1,1433;Database=MovimentosManuaisDb;User Id=sa;Password=Candidato123@;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
