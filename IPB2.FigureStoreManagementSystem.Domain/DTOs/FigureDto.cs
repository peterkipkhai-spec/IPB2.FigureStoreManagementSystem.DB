namespace IPB2.FigureStoreManagementSystem.Domain.DTOs;

public class FigureDto
{
    public int FigureId { get; set; }
    public string FigureName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
