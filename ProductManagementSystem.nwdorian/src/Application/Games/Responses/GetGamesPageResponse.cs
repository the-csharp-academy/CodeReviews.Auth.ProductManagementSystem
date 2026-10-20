namespace Application.Games.Responses;

public record class GetGamesPageResponse(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);
