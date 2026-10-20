namespace Application.Games.Commands;

public record class CreateGameCommand(string Name, string Genre, decimal Price, DateOnly ReleaseDate);
