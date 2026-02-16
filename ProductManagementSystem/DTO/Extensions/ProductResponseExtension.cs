using ProductManagementSystem.DTO.Products;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.DTO.Extensions;

public static class ProductResponseExtension
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Sku = product.Sku,
            Category = product.Category,
            Price = product.Price,
            DateAdded = product.DateAdded,
            Location = product.Location,
            IsActive = product.IsActive,
            Quantity = product.Quantity
        };
    }
}