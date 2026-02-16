using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Models;

public class Product
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
        ErrorMessage = "{0} length must be between {2} and {1} characters.",
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

    public int? Quantity { get; set; }

    public DateTime? DateAdded { get; set; }

    [StringLength(
        4,
        ErrorMessage = "{0} length must be exactly {1} characters long.",
        MinimumLength = 4
    )]
    public string? Location { get; set; }

    public bool IsActive { get; set; }
}