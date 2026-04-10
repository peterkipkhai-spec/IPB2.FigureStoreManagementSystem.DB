using IPB2.FigureStoreManagementSystem.Domain.Contracts;
using IPB2.FigureStoreManagementSystem.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.FigureStoreManagementSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiguresController(IFigureCatalogService figureCatalogService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFigures(CancellationToken cancellationToken)
    {
        var figures = await figureCatalogService.GetFiguresAsync(cancellationToken);
        return Ok(figures);
    }

    [HttpGet("{figureId:int}")]
    public async Task<IActionResult> GetFigureById(int figureId, CancellationToken cancellationToken)
    {
        var figure = await figureCatalogService.GetFigureByIdAsync(figureId, cancellationToken);
        return figure is null ? NotFound() : Ok(figure);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFigure([FromBody] FigureDto figure, CancellationToken cancellationToken)
    {
        var created = await figureCatalogService.CreateFigureAsync(figure, cancellationToken);
        return CreatedAtAction(nameof(GetFigureById), new { figureId = created.FigureId }, created);
    }

    [HttpPut("{figureId:int}")]
    public async Task<IActionResult> UpdateFigure(int figureId, [FromBody] FigureDto figure, CancellationToken cancellationToken)
    {
        var updated = await figureCatalogService.UpdateFigureAsync(figureId, figure, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{figureId:int}")]
    public async Task<IActionResult> DeleteFigure(int figureId, CancellationToken cancellationToken)
    {
        var deleted = await figureCatalogService.DeleteFigureAsync(figureId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
