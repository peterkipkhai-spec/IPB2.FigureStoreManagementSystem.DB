using IPB2.FigureStoreManagementSystem.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.FigureStoreManagementSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IFigureCatalogService figureCatalogService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await figureCatalogService.GetCategoriesAsync(cancellationToken);
        return Ok(categories);
    }
}
