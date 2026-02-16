using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.DTO.Products;

public class ProductResponse
{
    public int Id { get; set; }

    [Display(Name = "Product Name")] public string? ProductName { get; set; }

    [Display(Name = "SKU")] public string? Sku { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    [Display(Name = "Date Added")] public DateTime? DateAdded { get; set; }

    public string? Location { get; set; }

    [Display(Name = "Is Active?")] public bool IsActive { get; set; }

    public int? Quantity { get; set; }

    public StockStatus Status
    {
        get
        {
            if (Quantity == 0)
                return StockStatus.OutOfStock;
            if (Quantity < 10)
                return StockStatus.LowStock;
            return StockStatus.InStock;
        }
    }
}

public enum StockStatus
{
    [Display(Name = "In Stock")] InStock,

    [Display(Name = "Low Stock")] LowStock,

    [Display(Name = "Out of Stock")] OutOfStock
}