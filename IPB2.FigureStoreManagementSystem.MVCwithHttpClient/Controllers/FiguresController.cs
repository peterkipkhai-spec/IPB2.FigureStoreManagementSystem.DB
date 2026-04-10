using IPB2.FigureStoreManagementSystem.MVCwithHttpClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.FigureStoreManagementSystem.MVCwithHttpClient.Controllers;

public class FiguresController(FigureApiClient figureApiClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var figures = await figureApiClient.GetFiguresAsync(cancellationToken);
        return View(figures);
    }
}
