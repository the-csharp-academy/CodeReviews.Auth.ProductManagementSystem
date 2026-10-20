namespace Application.Games.Commands;

public record class UpdateGameCommand(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);
