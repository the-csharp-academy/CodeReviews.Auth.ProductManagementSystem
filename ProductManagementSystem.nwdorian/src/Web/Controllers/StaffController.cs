using Infrastructure.Authorization;
using Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Models.Staff;

namespace Web.Controllers;

public class StaffController(UserManager<User> userManager) : Controller
{
    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return PartialView(Partials.CreateStaff);
    }

    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StaffCreate model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView(Partials.CreateStaff, model);
        }

        User user = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
        };

        IdentityResult result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return PartialView(Partials.CreateStaff, model);
        }

        await userManager.AddToRoleAsync(user, Roles.Staff);

        return Created();
    }

    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        User? user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User was not found.");
            return PartialView(Partials.DeleteStaff, StaffDelete.Empty);
        }

        return PartialView(Partials.DeleteStaff, StaffDelete.Create(user));
    }

    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        User? user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User was not found.");
            return PartialView(Partials.DeleteStaff, StaffDelete.Empty);
        }

        IdentityResult result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return PartialView(Partials.DeleteStaff, StaffDelete.Create(user));
        }

        return NoContent();
    }

    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        User? user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User was not found.");
            return PartialView(Partials.UpdateStaff, StaffUpdate.Empty);
        }

        return PartialView(Partials.UpdateStaff, StaffUpdate.Create(user));
    }

    [Authorize(Policy = Policies.RequireAdmin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, StaffUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView(Partials.UpdateStaff, model);
        }

        User? user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User was not found.");
            return PartialView(Partials.UpdateStaff, StaffUpdate.Empty);
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;

        IdentityResult result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return PartialView(Partials.UpdateStaff, StaffUpdate.Create(user));
        }

        return NoContent();
    }
}
