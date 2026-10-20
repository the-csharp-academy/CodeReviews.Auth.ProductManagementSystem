using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Models.Games;

public class GamesCreate
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Genre { get; set; } = string.Empty;

    [JsonRequired]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be higher than 0")]
    public decimal Price { get; set; }

    [JsonRequired]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}
