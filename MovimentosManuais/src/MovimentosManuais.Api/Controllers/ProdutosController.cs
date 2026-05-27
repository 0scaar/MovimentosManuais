using Microsoft.AspNetCore.Mvc;
using MovimentosManuais.Application.Interfaces;

namespace MovimentosManuais.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public sealed class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAtivosAsync(
        CancellationToken cancellationToken)
    {
        var produtos = await _produtoService.ListarAtivosAsync(cancellationToken);

        return Ok(produtos);
    }
}
