using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.UnitTests.TestHelpers;

public static class DbContextTestExtensions
{
    public static async Task<ApplicationDbContext> ConfigureDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        var mockProductList = new List<Product>
        {
            new()
            {
                Id = 1,
                ProductName = "Keyboard",
                Category = "Hardware",
                Price = 12.99m,
                Quantity = 10,
                DateAdded = DateTime.Now,
                IsActive = true
            },
            new()
            {
                Id = 2,
                ProductName = "Mouse",
                Category = "Hardware",
                Price = 8.49m,
                Quantity = 25,
                DateAdded = DateTime.Now.AddHours(-12),
                IsActive = true
            },
            new()
            {
                Id = 3,
                ProductName = "Mona Lisa Painting",
                Category = "Decorations",
                Price = 199.99m,
                Quantity = 5,
                DateAdded = DateTime.Now.AddHours(-18),
                IsActive = false
            }
        };
        await context.AddRangeAsync(mockProductList);
        await context.SaveChangesAsync();

        return context;
    }
}