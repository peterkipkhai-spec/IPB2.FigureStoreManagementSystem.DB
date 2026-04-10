using IPB2.FigureStoreManagementSystem.Domain.DTOs;

namespace IPB2.FigureStoreManagementSystem.Domain.Contracts;

public interface IFigureCatalogService
{
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FigureDto>> GetFiguresAsync(CancellationToken cancellationToken = default);
    Task<FigureDto?> GetFigureByIdAsync(int figureId, CancellationToken cancellationToken = default);
    Task<FigureDto> CreateFigureAsync(FigureDto figure, CancellationToken cancellationToken = default);
    Task<bool> UpdateFigureAsync(int figureId, FigureDto figure, CancellationToken cancellationToken = default);
    Task<bool> DeleteFigureAsync(int figureId, CancellationToken cancellationToken = default);
}
