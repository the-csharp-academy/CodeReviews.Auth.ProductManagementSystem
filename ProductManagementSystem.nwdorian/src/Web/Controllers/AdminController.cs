using Infrastructure.Authorization;
using Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Admin;
using Web.Models.Staff.Items;

namespace Web.Controllers;

public class AdminController(UserManager<User> userManager) : Controller
{
    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IList<User> staff = await userManager.GetUsersInRoleAsync(Roles.Staff);

        return View(new AdminIndex { Staff = staff.Select(s => new StaffItem(s)).ToList() });
    }
}
