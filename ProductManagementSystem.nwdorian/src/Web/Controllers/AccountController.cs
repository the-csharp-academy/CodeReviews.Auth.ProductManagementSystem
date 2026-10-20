using Application.Emails;
using Application.Notifications;
using Infrastructure.Authorization;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Users;

namespace Web.Controllers;

public class AccountController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IEmailNotificationService emailNotificationService
) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        User user = new()
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            UserName = input.Email,
            Email = input.Email,
        };
        IdentityResult result = await userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(input);
        }

        string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        string? callback = Url.Action(
            nameof(ConfirmEmail),
            "Account",
            new { token, email = user.Email },
            Request.Scheme
        );

        RegisterConfirmationEmail registerConfirmationEmail = new(user.Email, callback!);
        await emailNotificationService.SendRegisterConfirmationEmail(registerConfirmationEmail);

        await userManager.AddToRoleAsync(user, Roles.Staff);

        return RedirectToAction(nameof(SuccessRegistration));
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        User? user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return View(nameof(Error));
        }

        IdentityResult result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return View(nameof(Error));
        }

        WelcomeEmail welcomeEmail = new(user.Email!);
        await emailNotificationService.SendWelcomeEmail(welcomeEmail);

        return View(nameof(ConfirmEmail));
    }

    [HttpGet]
    public IActionResult SuccessRegistration()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLogin input, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
            input.Email,
            input.Password,
            input.RememberMe,
            false
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login.");
            return View();
        }

        if (returnUrl is null)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdminLogin(string? returnUrl = null)
    {
        User? user = await userManager.FindByEmailAsync(UserFaker.AdminEmail);

        if (user is null)
        {
            return RedirectToAction(nameof(Error));
        }
        await signInManager.SignInAsync(user, isPersistent: false);
        if (returnUrl is null)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StaffLogin(string? returnUrl = null)
    {
        User? user = await userManager.FindByEmailAsync(UserFaker.StaffEmail);

        if (user is null)
        {
            return RedirectToAction(nameof(Error));
        }
        await signInManager.SignInAsync(user, isPersistent: false);
        if (returnUrl is null)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPassword input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        User? user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
        {
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);
        string? callback = Url.Action(
            nameof(ResetPassword),
            "Account",
            new { token, email = user.Email },
            Request.Scheme
        );

        PasswordResetEmail passwordResetEmail = new(user.Email!, callback!);
        await emailNotificationService.SendPasswordResetEmail(passwordResetEmail);

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        ResetPassword model = new() { Token = token, Email = email };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPassword input)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        User? user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        IdentityResult result = await userManager.ResetPasswordAsync(user, input.Token, input.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        return RedirectToAction(nameof(ResetPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}
