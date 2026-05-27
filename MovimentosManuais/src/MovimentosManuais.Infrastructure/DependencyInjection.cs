using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovimentosManuais.Application.Abstractions;
using MovimentosManuais.Domain.Repositories;
using MovimentosManuais.Infrastructure.Data;
using MovimentosManuais.Infrastructure.Repositories;

namespace MovimentosManuais.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("MovimentosManuaisDb")
            ?? configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'MovimentosManuaisDb' was not found.");
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoCosifRepository, ProdutoCosifRepository>();
        services.AddScoped<IMovimentoManualRepository, MovimentoManualRepository>();
        services.AddScoped<IMovimentoManualConsultaRepository, MovimentoManualConsultaRepository>();

        return services;
    }
}
