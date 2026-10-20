using System.ComponentModel.DataAnnotations;
using Application.Games.Responses;

namespace Web.Models.Games.Items;

public class GamesIndexItem(GetGamesPageResponse game)
{
    public Guid Id { get; set; } = game.Id;
    public string Name { get; set; } = game.Name;
    public string Genre { get; set; } = game.Genre;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; } = game.Price;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public DateOnly ReleaseDate { get; set; } = game.ReleaseDate;
}
