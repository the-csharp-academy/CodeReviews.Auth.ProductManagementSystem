using Application.Games.Commands;
using Application.Games.Queries;
using Application.Games.Responses;
using Application.Pagination;
using Domain.Core.Results;

namespace Application.Games;

public interface IGameService
{
    Task<PagedList<GetGamesPageResponse>> GetGamesPage(GetGamesPageQuery query, CancellationToken cancellationToken);
    Task<Result<GetGameByIdResponse>> GetById(GetGameByIdQuery query, CancellationToken cancellationToken);
    Task<Result> Create(CreateGameCommand command, CancellationToken cancellationToken);
    Task<Result> Delete(DeleteGameCommand command, CancellationToken cancellationToken);
    Task<Result> Update(UpdateGameCommand command, CancellationToken cancellationToken);
}
