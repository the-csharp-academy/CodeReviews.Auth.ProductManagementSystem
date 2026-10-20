using System.ComponentModel.DataAnnotations;
using Application.Games.Responses;

namespace Web.Models.Games;

public class GamesDelete
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Genre { get; set; }

    [DataType(DataType.Currency)]
    public required decimal Price { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public required DateOnly ReleaseDate { get; set; }

    public static GamesDelete Empty =>
        new()
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Genre = string.Empty,
            Price = 0,
            ReleaseDate = DateOnly.FromDateTime(DateTime.MinValue),
        };

    public static GamesDelete Create(GetGameByIdResponse game)
    {
        return new()
        {
            Id = game.Id,
            Name = game.Name,
            Genre = game.Genre,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }
}
