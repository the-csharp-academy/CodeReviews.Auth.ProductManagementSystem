using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.iamnikitakostin.Models;

namespace ProductManagementSystem.iamnikitakostin.Data;

public class Seeding
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roles = new[] { "Admin", "Manager", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        string email = "admin@admin.com";
        string password = "1234";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new IdentityUser();
            user.UserName = email;
            user.Email = email;
            user.EmailConfirmed = true;

            await userManager.CreateAsync(user, password);

            await userManager.AddToRoleAsync(user, "Admin");
        }

        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Products.Any())
        {
            for(int i = 0; i < 4; i++)
            {
                var product = new Product();
                product.Price = i + 1;
                product.Name = "product" + i.ToString();
                product.Description = "description" + i.ToString();
                product.DateAdded = DateTime.Now.AddDays(i);
                product.DateUpdated = DateTime.Now.AddDays(i);
                product.IsActive = true;

                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}