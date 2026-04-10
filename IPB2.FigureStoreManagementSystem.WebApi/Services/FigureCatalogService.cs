using IPB2.FigureStoreManagementSystem.DB.Models;
using IPB2.FigureStoreManagementSystem.Domain.Contracts;
using IPB2.FigureStoreManagementSystem.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IPB2.FigureStoreManagementSystem.WebApi.Services;

public class FigureCatalogService(FigureStoreDbContext dbContext) : IFigureCatalogService
{
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.FsCategories
            .AsNoTracking()
            .OrderBy(c => c.CategoryName)
            .Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName ?? string.Empty
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<FigureDto>> GetFiguresAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.FsFigures
            .AsNoTracking()
            .Include(f => f.Category)
            .OrderBy(f => f.FigureName)
            .Select(f => MapToDto(f))
            .ToListAsync(cancellationToken);
    }

    public async Task<FigureDto?> GetFigureByIdAsync(int figureId, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.FsFigures
            .AsNoTracking()
            .Include(f => f.Category)
            .FirstOrDefaultAsync(f => f.FigureId == figureId, cancellationToken);

        return entity is null ? null : MapToDto(entity);
    }

    public async Task<FigureDto> CreateFigureAsync(FigureDto figure, CancellationToken cancellationToken = default)
    {
        var entity = new FsFigure
        {
            FigureName = figure.FigureName,
            Description = figure.Description,
            Price = figure.Price,
            Stock = figure.Stock,
            CategoryId = figure.CategoryId
        };

        dbContext.FsFigures.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return (await GetFigureByIdAsync(entity.FigureId, cancellationToken))!;
    }

    public async Task<bool> UpdateFigureAsync(int figureId, FigureDto figure, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.FsFigures.FirstOrDefaultAsync(f => f.FigureId == figureId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.FigureName = figure.FigureName;
        entity.Description = figure.Description;
        entity.Price = figure.Price;
        entity.Stock = figure.Stock;
        entity.CategoryId = figure.CategoryId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteFigureAsync(int figureId, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.FsFigures.FirstOrDefaultAsync(f => f.FigureId == figureId, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        dbContext.FsFigures.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static FigureDto MapToDto(FsFigure f)
    {
        return new FigureDto
        {
            FigureId = f.FigureId,
            FigureName = f.FigureName ?? string.Empty,
            Description = f.Description,
            Price = f.Price ?? 0,
            Stock = f.Stock ?? 0,
            CategoryId = f.CategoryId,
            CategoryName = f.Category?.CategoryName
        };
    }
}
