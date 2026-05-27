using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Interfaces;

namespace MovimentosManuais.Api.Controllers;

[ApiController]
[Route("api/produtos/{codigoProduto}/cosifs")]
public sealed class ProdutosCosifController : ControllerBase
{
    private readonly IProdutoCosifService _produtoCosifService;

    public ProdutosCosifController(
        IProdutoCosifService produtoCosifService)
    {
        _produtoCosifService = produtoCosifService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarPorProdutoAsync(
        string codigoProduto,
        CancellationToken cancellationToken)
    {
        var cosifs = await _produtoCosifService.ListarAtivosPorProdutoAsync(
            codigoProduto,
            cancellationToken);

        return Ok(cosifs);
    }
}
