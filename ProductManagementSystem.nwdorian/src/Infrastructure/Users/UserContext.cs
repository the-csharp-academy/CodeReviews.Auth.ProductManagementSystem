using Application.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId =>
        httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true
            ? httpContextAccessor.HttpContext.User.GetUserId()
            : UserFaker.SystemId;
}
