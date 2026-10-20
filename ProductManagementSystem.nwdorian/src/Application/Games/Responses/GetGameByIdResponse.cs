namespace Application.Games.Responses;

public record class GetGameByIdResponse(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);
