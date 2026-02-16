using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.DTO.Products;

public class ProductUpdateRequest
{
    public int Id { get; set; }

    [Display(Name = "Product Name")]
    [StringLength(
        50,
        ErrorMessage = "{0} length must be between {2} and {1} characters.",
        MinimumLength = 3
    )]
    public string? ProductName { get; set; }

    [StringLength(
        10,
        ErrorMessage = "{0} length must be between {2} and {1} characters. Example: TEST-0001",
        MinimumLength = 8
    )]
    public string? Sku { get; set; }

    [StringLength(
        20,
        ErrorMessage = "{0} length must be between {2} and {1} characters.",
        MinimumLength = 3
    )]
    public string? Category { get; set; }

    [Column(TypeName = "decimal(18, 2)")] public decimal? Price { get; set; }

    public DateTime? DateAdded { get; set; }

    [StringLength(
        4,
        ErrorMessage = "{0} length must be exactly {1} characters long. Example: A-01",
        MinimumLength = 4
    )]
    public string? Location { get; set; }

    public bool IsActive { get; set; }

    public int? Quantity { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            ProductName = ProductName,
            Sku = Sku,
            Category = Category,
            Price = Price,
            DateAdded = DateAdded,
            Location = Location,
            IsActive = IsActive,
            Quantity = Quantity
        };
    }
}