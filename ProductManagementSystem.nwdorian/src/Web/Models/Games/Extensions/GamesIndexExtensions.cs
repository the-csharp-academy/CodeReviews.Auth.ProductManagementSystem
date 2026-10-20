using System.Globalization;

namespace Web.Models.Games.Extensions;

public static class GamesIndexExtensions
{
    public static Dictionary<string, string?> PagingRouteValues(this GamesIndex model)
    {
        return new()
        {
            [nameof(model.PagingMetadata.PageSize)] = model.PagingMetadata.PageSize.ToString(
                CultureInfo.InvariantCulture
            ),
        };
    }
}
