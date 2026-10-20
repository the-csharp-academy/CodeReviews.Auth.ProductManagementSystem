using Application.Games;
using Application.Games.Commands;
using Application.Games.Pagination;
using Application.Games.Queries;
using Application.Games.Responses;
using Application.Pagination;
using Domain.Core.Results;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Models.Games;
using Web.Models.Games.Requests;

namespace Web.Controllers;

public class GamesController(IGameService gameService) : Controller
{
    [Authorize(Policy = Policies.RequireStaff)]
    [HttpGet]
    public async Task<IActionResult> Index(GetGamesRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        GameFilter filter = new(request.SearchTerm);
        GameSorting sorting = new(request.SortBy, request.SortDirection);
        Paging paging = new(request.PageNumber, request.PageSize);
        GetGamesPageQuery query = new(filter, sorting, paging);

        PagedList<GetGamesPageResponse> page = await gameService.GetGamesPage(query, cancellationToken);
        return View(GamesIndex.Create(page, request));
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return PartialView(Partials.CreateGame, new GamesCreate());
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GamesCreate model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return PartialView(Partials.CreateGame, model);
        }

        CreateGameCommand command = new(model.Name, model.Genre, model.Price, model.ReleaseDate);
        Result create = await gameService.Create(command, cancellationToken);
        if (create.IsFailure)
        {
            ModelState.AddModelError(string.Empty, create.Error.Description);
            return PartialView(Partials.CreateGame, model);
        }

        return Created();
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        Result<GetGameByIdResponse> getById = await gameService.GetById(new GetGameByIdQuery(id), cancellationToken);
        if (getById.IsFailure)
        {
            ModelState.AddModelError(string.Empty, getById.Error.Description);
            return PartialView(Partials.DeleteGame, GamesDelete.Empty);
        }

        return PartialView(Partials.DeleteGame, GamesDelete.Create(getById.Value));
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        Result delete = await gameService.Delete(new DeleteGameCommand(id), cancellationToken);
        if (delete.IsFailure)
        {
            ModelState.AddModelError(string.Empty, delete.Error.Description);
            return PartialView(Partials.DeleteGame, GamesDelete.Empty);
        }

        return NoContent();
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpGet]
    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        Result<GetGameByIdResponse> getById = await gameService.GetById(new GetGameByIdQuery(id), cancellationToken);
        if (getById.IsFailure)
        {
            ModelState.AddModelError(string.Empty, getById.Error.Description);
            return PartialView(Partials.UpdateGame, GamesUpdate.Empty);
        }

        return PartialView(Partials.UpdateGame, GamesUpdate.Create(getById.Value));
    }

    [Authorize(Policy = Policies.RequireStaff)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, GamesUpdate model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return PartialView(Partials.UpdateGame, model);
        }

        Result update = await gameService.Update(
            new UpdateGameCommand(id, model.Name, model.Genre, model.Price, model.ReleaseDate),
            cancellationToken
        );
        if (update.IsFailure)
        {
            ModelState.AddModelError(string.Empty, update.Error.Description);
            return PartialView(Partials.UpdateGame, model);
        }

        return NoContent();
    }
}
