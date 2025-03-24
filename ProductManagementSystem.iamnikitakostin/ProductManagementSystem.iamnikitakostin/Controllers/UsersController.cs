using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.iamnikitakostin.Data;
using ProductManagementSystem.iamnikitakostin.Models;

namespace ProductManagementSystem.iamnikitakostin.Controllers;
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ProductsController> _logger;

    public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<ProductsController> logger)
    {
        _context = context;
        _userManager = userManager;        _logger = logger;
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var userRoles = new List<UserForView>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRoles.Add(new UserForView
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles.ToList(),
                SelectedRole = roles[0]
            });
        }

        return View(userRoles);
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            _logger.LogError($"The user id cannot be null.");

            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            _logger.LogError($"The user with the Id of {id} has not been found");

            return NotFound();
        }

        var userView = new UserForView();
        var roles = await _userManager.GetRolesAsync(user);

        userView.UserName = user.UserName;
        userView.Id = user.Id;
        userView.Roles = new List<string> { "Admin", "Manager", "User" };
        userView.SelectedRole = roles[0];

        return View(userView);
    }

    // GET: Users/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserName, SelectedRoles")] UserForView user)
    {
        var newUser = new IdentityUser();
        newUser.UserName = user.UserName;
        newUser.Email = user.UserName;
        newUser.EmailConfirmed = true;

        if (ModelState.IsValid)
        {
            _context.Add(newUser);
            await _context.SaveChangesAsync();

            await _userManager.AddToRoleAsync(newUser, "User");

            _logger.LogInformation($"The user with the username of {user.UserName} has been created");

            return RedirectToAction(nameof(Index));
        }
        _logger.LogError($"The user with the username of {user.UserName} has not been created due to an error.");

        return View(user);
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            _logger.LogError($"The user id cannot be null.");

            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            _logger.LogError($"The user with the Id of {id} has not been found");

            return NotFound();
        }

        var userView = new UserForView();
        var roles = await _userManager.GetRolesAsync(user);

        userView.UserName = user.UserName;
        userView.Id = user.Id;
        userView.Roles = new List<string>{ "Admin", "Manager", "User"};
        userView.SelectedRole = roles[0];

        return View(userView);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,UserName, SelectedRoles")] UserForView user)
    {
        if (id != user.Id)
        {
            _logger.LogError($"The user id cannot be null.");

            return NotFound();
        }

        IdentityUser oldUser = await _context.Users.FindAsync(user.Id);

        var currentRoles = await _userManager.GetRolesAsync(oldUser);

        if (!currentRoles.ToList().Contains(user.SelectedRole))
        {
            await _userManager.RemoveFromRolesAsync(oldUser, currentRoles);
            await _userManager.AddToRoleAsync(oldUser, user.SelectedRole);
        }

        oldUser.UserName = user.UserName;

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(oldUser);
                await _context.SaveChangesAsync();

                _logger.LogError($"The user with the username of {user.UserName} has been updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    _logger.LogError($"The user with the username of {user.UserName} has not been updated.");

                    return NotFound();
                }
                else
                {
                    _logger.LogError($"The user with the username of {user.UserName} has not been updated due to a critical error.");

                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            _logger.LogError($"Id of the user cannot be null.");

            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            _logger.LogError($"The user with the id of {id} has not been found.");

            return NotFound();
        }

        return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }

        await _context.SaveChangesAsync();

        _logger.LogError($"The user with the id of {id} has been deleted.");

        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(string id)
    {
        return _context.Users.Any(e => e.Id == id.ToString());
    }
}
