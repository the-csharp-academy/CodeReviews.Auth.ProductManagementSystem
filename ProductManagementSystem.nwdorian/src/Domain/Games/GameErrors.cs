using Domain.Core.Results;

namespace Domain.Games;

public static class GameErrors
{
    public static Error NameAlreadyExists(string name) =>
        Error.Conflict($"{nameof(Game)}.{nameof(NameAlreadyExists)}", $"Game with the name {name} already exists.");

    public static Error NotFoundById(Guid id) =>
        Error.NotFound($"{nameof(Game)}.{nameof(NotFoundById)}", $"The game with Id = {id} was not found.");
}
