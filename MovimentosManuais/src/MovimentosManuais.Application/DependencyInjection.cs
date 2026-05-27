using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Application.Services;
using System.Reflection;

namespace MovimentosManuais.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoCosifService, ProdutoCosifService>();
        services.AddScoped<IMovimentoManualService, MovimentoManualService>();

        return services;
    }
}
