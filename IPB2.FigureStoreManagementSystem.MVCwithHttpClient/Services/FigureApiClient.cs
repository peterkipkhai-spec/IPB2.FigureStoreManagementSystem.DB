using IPB2.FigureStoreManagementSystem.MVCwithHttpClient.Models;

namespace IPB2.FigureStoreManagementSystem.MVCwithHttpClient.Services;

public class FigureApiClient(HttpClient httpClient)
{
    public async Task<IReadOnlyList<FigureViewModel>> GetFiguresAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<FigureViewModel>>("api/figures", cancellationToken);
        return result ?? [];
    }
}
