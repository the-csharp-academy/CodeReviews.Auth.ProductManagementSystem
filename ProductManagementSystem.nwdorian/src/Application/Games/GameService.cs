using Application.Database;
using Application.Games.Commands;
using Application.Games.Queries;
using Application.Games.Responses;
using Application.Pagination;
using Domain.Core.Results;
using Domain.Games;
using Microsoft.EntityFrameworkCore;

namespace Application.Games;

public class GameService(IApplicationDbContext dbContext) : IGameService
{
    public async Task<PagedList<GetGamesPageResponse>> GetGamesPage(
        GetGamesPageQuery query,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Game> games = dbContext.Games;

        games = games.ConditionalWhere(
            !string.IsNullOrWhiteSpace(query.Filter.SearchTerm),
            g => g.Name.Contains(query.Filter.SearchTerm!)
        );

        games = games.ApplySorting(query.Sorting.GetSortColumn(), query.Sorting.SortDirection);

        IQueryable<GetGamesPageResponse> gameResponses = games.Select(g => new GetGamesPageResponse(
            g.Id,
            g.Name,
            g.Genre,
            g.Price,
            g.ReleaseDate
        ));

        return await PagedList<GetGamesPageResponse>.Create(gameResponses, query.Paging, cancellationToken);
    }

    public async Task<Result<GetGameByIdResponse>> GetById(GetGameByIdQuery query, CancellationToken cancellationToken)
    {
        GetGameByIdResponse? game = await dbContext
            .Games.Where(g => g.Id == query.Id)
            .Select(g => new GetGameByIdResponse(g.Id, g.Name, g.Genre, g.Price, g.ReleaseDate))
            .FirstOrDefaultAsync(cancellationToken);

        if (game is null)
        {
            return GameErrors.NotFoundById(query.Id);
        }

        return game;
    }

    public async Task<Result> Create(CreateGameCommand command, CancellationToken cancellationToken)
    {
        bool nameExists = await dbContext.Games.AnyAsync(g => g.Name == command.Name, cancellationToken);
        if (nameExists)
        {
            return GameErrors.NameAlreadyExists(command.Name);
        }

        Game game = new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Genre = command.Genre,
            Price = command.Price,
            ReleaseDate = command.ReleaseDate,
        };

        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Delete(DeleteGameCommand command, CancellationToken cancellationToken)
    {
        Game? game = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == command.Id, cancellationToken);
        if (game is null)
        {
            return GameErrors.NotFoundById(command.Id);
        }

        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Update(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        Game? game = await dbContext.Games.AsTracking().FirstOrDefaultAsync(g => g.Id == command.Id, cancellationToken);
        if (game is null)
        {
            return GameErrors.NotFoundById(command.Id);
        }

        if (await dbContext.Games.AnyAsync(g => g.Name == command.Name && g.Id != command.Id, cancellationToken))
        {
            return GameErrors.NameAlreadyExists(command.Name);
        }

        game.Name = command.Name;
        game.Genre = command.Genre;
        game.Price = command.Price;
        game.ReleaseDate = command.ReleaseDate;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
