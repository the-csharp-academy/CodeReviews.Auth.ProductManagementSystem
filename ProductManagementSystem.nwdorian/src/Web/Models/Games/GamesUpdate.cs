using System.ComponentModel.DataAnnotations;
using Application.Games.Responses;

namespace Web.Models.Games;

public class GamesUpdate
{
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Genre { get; set; }

    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be higher than 0")]
    public required decimal Price { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public required DateOnly ReleaseDate { get; set; }

    public static GamesUpdate Empty =>
        new()
        {
            Name = string.Empty,
            Genre = string.Empty,
            Price = 0,
            ReleaseDate = DateOnly.FromDateTime(DateTime.MinValue),
        };

    public static GamesUpdate Create(GetGameByIdResponse game)
    {
        return new()
        {
            Name = game.Name,
            Genre = game.Genre,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }
}
