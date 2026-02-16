using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Data;

public static class SeedData
{
    public static async Task InitializeDataAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        string[] roleNames = { "Admin", "Manager", "Employee" };

        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
        );

        // Ensure database exists
        await context.Database.MigrateAsync();

        // Seed Roles
        foreach (var roleName in roleNames)
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));

        const string adminEmail = "admin@test.com";
        const string employeeEmail = "test@test.com";
        const string seedPassword = "Password1!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, seedPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
        if (employeeUser is null)
        {
            employeeUser = new ApplicationUser
            {
                UserName = employeeEmail,
                Email = employeeEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(employeeUser, seedPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to create employee user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            await userManager.AddToRoleAsync(adminUser, "Admin");

        if (!await userManager.IsInRoleAsync(employeeUser, "Employee"))
            await userManager.AddToRoleAsync(employeeUser, "Employee");

        // If the table already contains products data, skip seeding
        if (await context.Products.AnyAsync())
            return;

        var products = new List<Product>
        {
            new()
            {
                ProductName = "Wireless Mouse",
                Sku = "ELEC-0001",
                Category = "Electronics",
                Price = 29.99m,
                Quantity = 150,
                DateAdded = new DateTime(2025, 6, 1),
                Location = "A-01",
                IsActive = true
            },
            new()
            {
                ProductName = "Mechanical Keyboard",
                Sku = "ELEC-0002",
                Category = "Electronics",
                Price = 89.50m,
                Quantity = 75,
                DateAdded = new DateTime(2025, 5, 20),
                Location = "A-02",
                IsActive = true
            },
            new()
            {
                ProductName = "Stainless Steel Water Bottle",
                Sku = "HOME-0001",
                Category = "Home",
                Price = 19.95m,
                Quantity = 200,
                DateAdded = new DateTime(2025, 2, 10),
                Location = "B-03",
                IsActive = true
            },
            new()
            {
                ProductName = "Organic Green Tea (100 bags)",
                Sku = "GROC-0001",
                Category = "Grocery",
                Price = 12.00m,
                Quantity = 500,
                DateAdded = new DateTime(2024, 11, 15),
                Location = "C-04",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Denim Jacket",
                Sku = "CLOT-0001",
                Category = "Clothing",
                Price = 59.99m,
                Quantity = 40,
                DateAdded = new DateTime(2025, 3, 5),
                Location = "D-05",
                IsActive = true
            },
            new()
            {
                ProductName = "Children's Building Blocks Set",
                Sku = "TOYS-0001",
                Category = "Toys",
                Price = 24.99m,
                Quantity = 120,
                DateAdded = new DateTime(2025, 4, 12),
                Location = "E-06",
                IsActive = true
            },
            new()
            {
                ProductName = "Yoga Mat - Non Slip",
                Sku = "SPRT-0001",
                Category = "Sports",
                Price = 34.00m,
                Quantity = 80,
                DateAdded = new DateTime(2025, 1, 18),
                Location = "F-07",
                IsActive = true
            },
            new()
            {
                ProductName = "Noise-Cancelling Headphones",
                Sku = "ELEC-0003",
                Category = "Electronics",
                Price = 199.99m,
                Quantity = 25,
                DateAdded = new DateTime(2024, 12, 1),
                Location = "A-08",
                IsActive = false
            },
            new()
            {
                ProductName = "Hardcover Notebook - 200 pages",
                Sku = "BOOK-0001",
                Category = "Books",
                Price = 8.50m,
                Quantity = 300,
                DateAdded = new DateTime(2025, 6, 10),
                Location = "G-09",
                IsActive = true
            },
            new()
            {
                ProductName = "Vitamin D 1000IU - 120 capsules",
                Sku = "HLTH-0001",
                Category = "Health",
                Price = 14.75m,
                Quantity = 60,
                DateAdded = new DateTime(2024, 9, 30),
                Location = "H-10",
                IsActive = true
            },
            new()
            {
                ProductName = "Smartphone Car Mount",
                Sku = "AUTO-0001",
                Category = "Automotive",
                Price = 15.99m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 2, 2),
                Location = "I-11",
                IsActive = true
            },
            new()
            {
                ProductName = "LED Desk Lamp with USB",
                Sku = "HOME-0002",
                Category = "Home",
                Price = 27.49m,
                Quantity = 140,
                DateAdded = new DateTime(2025, 3, 14),
                Location = "B-12",
                IsActive = true
            },
            new()
            {
                ProductName = "Ceramic Coffee Mug - 12oz",
                Sku = "KTCH-0001",
                Category = "Kitchen",
                Price = 9.99m,
                Quantity = 360,
                DateAdded = new DateTime(2024, 10, 5),
                Location = "J-13",
                IsActive = true
            },
            new()
            {
                ProductName = "Wireless Charger Pad",
                Sku = "ELEC-0004",
                Category = "Electronics",
                Price = 22.00m,
                Quantity = 180,
                DateAdded = new DateTime(2025, 4, 1),
                Location = "A-14",
                IsActive = true
            },
            new()
            {
                ProductName = "Eco-friendly Bamboo Toothbrush (4-pack)",
                Sku = "HLTH-0002",
                Category = "Health",
                Price = 7.50m,
                Quantity = 420,
                DateAdded = new DateTime(2024, 8, 20),
                Location = "H-15",
                IsActive = true
            },
            new()
            {
                ProductName = "Adjustable Laptop Stand",
                Sku = "OFFC-0001",
                Category = "Office",
                Price = 39.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 1, 7),
                Location = "K-16",
                IsActive = true
            },
            new()
            {
                ProductName = "Stain-Resistant Sofa Cover - Large",
                Sku = "HOME-0003",
                Category = "Home",
                Price = 64.99m,
                Quantity = 50,
                DateAdded = new DateTime(2025, 5, 25),
                Location = "B-17",
                IsActive = true
            },
            new()
            {
                ProductName = "Kids' Puzzle 500 pcs",
                Sku = "TOYS-0002",
                Category = "Toys",
                Price = 14.99m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 2, 28),
                Location = "E-18",
                IsActive = true
            },
            new()
            {
                ProductName = "Trail Running Shoes - Men's 10",
                Sku = "SHOE-0001",
                Category = "Shoes",
                Price = 89.99m,
                Quantity = 30,
                DateAdded = new DateTime(2025, 3, 30),
                Location = "L-19",
                IsActive = true
            },
            new()
            {
                ProductName = "Stainless Cutlery Set - 16pc",
                Sku = "KTCH-0002",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 110,
                DateAdded = new DateTime(2024, 12, 18),
                Location = "J-20",
                IsActive = true
            },
            new()
            {
                ProductName = "Bluetooth Speaker - Waterproof",
                Sku = "ELEC-0005",
                Category = "Electronics",
                Price = 49.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 25),
                Location = "A-21",
                IsActive = true
            },
            new()
            {
                ProductName = "Portable Power Bank 20000mAh",
                Sku = "ELEC-0006",
                Category = "Electronics",
                Price = 39.50m,
                Quantity = 130,
                DateAdded = new DateTime(2024, 11, 2),
                Location = "A-22",
                IsActive = true
            },
            new()
            {
                ProductName = "Non-stick Frying Pan 10 inch",
                Sku = "KTCH-0003",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 70,
                DateAdded = new DateTime(2025, 4, 8),
                Location = "J-23",
                IsActive = true
            },
            new()
            {
                ProductName = "Desk Organizer - Multi Compartment",
                Sku = "OFFC-0002",
                Category = "Office",
                Price = 12.99m,
                Quantity = 250,
                DateAdded = new DateTime(2025, 5, 2),
                Location = "K-24",
                IsActive = true
            },
            new()
            {
                ProductName = "Garden Hose 50ft",
                Sku = "GRDN-0001",
                Category = "Garden",
                Price = 29.95m,
                Quantity = 60,
                DateAdded = new DateTime(2024, 9, 12),
                Location = "M-25",
                IsActive = true
            },
            new()
            {
                ProductName = "Pet Bed - Medium",
                Sku = "PETS-0001",
                Category = "Pet Supplies",
                Price = 22.00m,
                Quantity = 90,
                DateAdded = new DateTime(2025, 2, 15),
                Location = "N-26",
                IsActive = true
            },
            new()
            {
                ProductName = "Children's Raincoat - Age 4-6",
                Sku = "CLOT-0002",
                Category = "Clothing",
                Price = 19.99m,
                Quantity = 75,
                DateAdded = new DateTime(2025, 3, 20),
                Location = "D-27",
                IsActive = true
            },
            new()
            {
                ProductName = "Electric Kettle 1.7L",
                Sku = "KTCH-0004",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 48,
                DateAdded = new DateTime(2024, 12, 6),
                Location = "J-28",
                IsActive = true
            },
            new()
            {
                ProductName = "Metal Screwdriver Set - 10pc",
                Sku = "TOOL-0001",
                Category = "Tools",
                Price = 17.50m,
                Quantity = 160,
                DateAdded = new DateTime(2025, 1, 12),
                Location = "O-29",
                IsActive = true
            },
            new()
            {
                ProductName = "Classic Vinyl Record - Rock Hits",
                Sku = "MUSC-0001",
                Category = "Music",
                Price = 21.00m,
                Quantity = 40,
                DateAdded = new DateTime(2024, 10, 28),
                Location = "P-30",
                IsActive = true
            },
            new()
            {
                ProductName = "Streaming Media Player",
                Sku = "ELEC-0007",
                Category = "Electronics",
                Price = 59.99m,
                Quantity = 55,
                DateAdded = new DateTime(2025, 6, 5),
                Location = "A-31",
                IsActive = true
            },
            new()
            {
                ProductName = "Silicone Baking Mat - Set of 2",
                Sku = "KTCH-0005",
                Category = "Kitchen",
                Price = 13.99m,
                Quantity = 140,
                DateAdded = new DateTime(2025, 4, 20),
                Location = "J-32",
                IsActive = true
            },
            new()
            {
                ProductName = "Women's Lightweight Scarf",
                Sku = "ACCS-0001",
                Category = "Accessories",
                Price = 11.50m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 2, 27),
                Location = "Q-33",
                IsActive = true
            },
            new()
            {
                ProductName = "Board Game - Strategy Edition",
                Sku = "GAME-0001",
                Category = "Games",
                Price = 44.99m,
                Quantity = 65,
                DateAdded = new DateTime(2025, 3, 9),
                Location = "R-34",
                IsActive = true
            },
            new()
            {
                ProductName = "Reusable Grocery Tote - 3 pack",
                Sku = "HOME-0004",
                Category = "Home",
                Price = 9.50m,
                Quantity = 300,
                DateAdded = new DateTime(2024, 11, 22),
                Location = "B-35",
                IsActive = true
            },
            new()
            {
                ProductName = "Camping Lantern - LED",
                Sku = "OUTD-0001",
                Category = "Outdoors",
                Price = 18.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 5, 18),
                Location = "S-36",
                IsActive = true
            },
            new()
            {
                ProductName = "Leather Card Wallet",
                Sku = "ACCS-0002",
                Category = "Accessories",
                Price = 24.00m,
                Quantity = 120,
                DateAdded = new DateTime(2025, 1, 5),
                Location = "Q-37",
                IsActive = true
            },
            new()
            {
                ProductName = "Anti-Fog Swim Goggles",
                Sku = "SPRT-0002",
                Category = "Sports",
                Price = 12.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 9, 5),
                Location = "F-38",
                IsActive = true
            },
            new()
            {
                ProductName = "Wireless Presentation Remote",
                Sku = "OFFC-0003",
                Category = "Office",
                Price = 16.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 2, 9),
                Location = "K-39",
                IsActive = true
            },
            new()
            {
                ProductName = "Aromatic Soy Candle - Lavender",
                Sku = "HOME-0005",
                Category = "Home",
                Price = 11.99m,
                Quantity = 200,
                DateAdded = new DateTime(2025, 4, 3),
                Location = "B-40",
                IsActive = true
            },
            new()
            {
                ProductName = "4K Action Camera",
                Sku = "ELEC-0008",
                Category = "Electronics",
                Price = 299.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 15),
                Location = "A-41",
                IsActive = false
            },
            new()
            {
                ProductName = "Ergonomic Office Chair",
                Sku = "FURN-0001",
                Category = "Furniture",
                Price = 249.00m,
                Quantity = 25,
                DateAdded = new DateTime(2025, 1, 20),
                Location = "T-42",
                IsActive = true
            },
            new()
            {
                ProductName = "Cotton Bath Towel Set - 6pc",
                Sku = "HOME-0006",
                Category = "Home",
                Price = 39.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 12, 5),
                Location = "B-43",
                IsActive = true
            },
            new()
            {
                ProductName = "Protein Powder - Chocolate 2lb",
                Sku = "HLTH-0003",
                Category = "Health",
                Price = 34.95m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 8, 18),
                Location = "H-44",
                IsActive = true
            },
            new()
            {
                ProductName = "Digital Alarm Clock",
                Sku = "HOME-0007",
                Category = "Home",
                Price = 19.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 3, 12),
                Location = "B-45",
                IsActive = true
            },
            new()
            {
                ProductName = "Stainless Steel Cookware Set - 12pc",
                Sku = "KTCH-0006",
                Category = "Kitchen",
                Price = 189.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 10, 22),
                Location = "J-46",
                IsActive = false
            },
            new()
            {
                ProductName = "Women's Running Tank Top",
                Sku = "CLOT-0003",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 4, 7),
                Location = "D-47",
                IsActive = true
            },
            new()
            {
                ProductName = "Cordless Drill Set",
                Sku = "TOOL-0002",
                Category = "Tools",
                Price = 119.99m,
                Quantity = 35,
                DateAdded = new DateTime(2024, 11, 30),
                Location = "O-48",
                IsActive = true
            },
            new()
            {
                ProductName = "Gel Pen Set - 24 Colors",
                Sku = "OFFC-0004",
                Category = "Office",
                Price = 15.50m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 2, 18),
                Location = "K-49",
                IsActive = true
            },
            new()
            {
                ProductName = "Dog Chew Toys - 5 Pack",
                Sku = "PETS-0002",
                Category = "Pet Supplies",
                Price = 16.99m,
                Quantity = 155,
                DateAdded = new DateTime(2025, 1, 28),
                Location = "N-50",
                IsActive = true
            },
            new()
            {
                ProductName = "Decorative Throw Pillows - Set of 4",
                Sku = "HOME-0008",
                Category = "Home",
                Price = 44.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 9, 9),
                Location = "B-51",
                IsActive = true
            },
            new()
            {
                ProductName = "Basketball - Official Size",
                Sku = "SPRT-0003",
                Category = "Sports",
                Price = 29.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 7, 25),
                Location = "F-52",
                IsActive = false
            },
            new()
            {
                ProductName = "Wall-Mounted Coat Rack",
                Sku = "FURN-0002",
                Category = "Furniture",
                Price = 32.50m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 3, 3),
                Location = "T-53",
                IsActive = true
            },
            new()
            {
                ProductName = "Organic Honey - 16oz",
                Sku = "GROC-0002",
                Category = "Grocery",
                Price = 12.99m,
                Quantity = 240,
                DateAdded = new DateTime(2024, 12, 12),
                Location = "C-54",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Leather Belt",
                Sku = "ACCS-0003",
                Category = "Accessories",
                Price = 29.99m,
                Quantity = 115,
                DateAdded = new DateTime(2025, 2, 22),
                Location = "Q-55",
                IsActive = true
            },
            new()
            {
                ProductName = "Tablet Stand - Adjustable",
                Sku = "ELEC-0009",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 8, 5),
                Location = "A-56",
                IsActive = true
            },
            new()
            {
                ProductName = "Toddler Puzzle Set - Animals",
                Sku = "TOYS-0003",
                Category = "Toys",
                Price = 18.99m,
                Quantity = 130,
                DateAdded = new DateTime(2025, 1, 15),
                Location = "E-57",
                IsActive = true
            },
            new()
            {
                ProductName = "Facial Moisturizer - SPF 30",
                Sku = "BEUT-0001",
                Category = "Beauty",
                Price = 22.50m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 11, 8),
                Location = "U-58",
                IsActive = true
            },
            new()
            {
                ProductName = "Car Phone Holder - Magnetic",
                Sku = "AUTO-0002",
                Category = "Automotive",
                Price = 12.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 10, 15),
                Location = "I-59",
                IsActive = false
            },
            new()
            {
                ProductName = "Microfiber Cleaning Cloths - 12 Pack",
                Sku = "HOME-0009",
                Category = "Home",
                Price = 11.99m,
                Quantity = 320,
                DateAdded = new DateTime(2025, 5, 5),
                Location = "B-60",
                IsActive = true
            },
            new()
            {
                ProductName = "Gaming Mouse Pad - XL",
                Sku = "ELEC-0010",
                Category = "Electronics",
                Price = 19.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 4, 15),
                Location = "A-61",
                IsActive = true
            },
            new()
            {
                ProductName = "Hiking Backpack - 40L",
                Sku = "OUTD-0002",
                Category = "Outdoors",
                Price = 79.99m,
                Quantity = 22,
                DateAdded = new DateTime(2024, 9, 18),
                Location = "S-62",
                IsActive = true
            },
            new()
            {
                ProductName = "Essential Oil Diffuser",
                Sku = "HOME-0010",
                Category = "Home",
                Price = 29.99m,
                Quantity = 142,
                DateAdded = new DateTime(2025, 3, 25),
                Location = "B-63",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Wool Socks - 6 Pair",
                Sku = "CLOT-0004",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 10),
                Location = "D-64",
                IsActive = false
            },
            new()
            {
                ProductName = "Electric Toothbrush",
                Sku = "HLTH-0004",
                Category = "Health",
                Price = 49.99m,
                Quantity = 68,
                DateAdded = new DateTime(2025, 2, 5),
                Location = "H-65",
                IsActive = true
            },
            new()
            {
                ProductName = "Non-Slip Rug Pad - 5x7",
                Sku = "HOME-0011",
                Category = "Home",
                Price = 19.99m,
                Quantity = 175,
                DateAdded = new DateTime(2024, 12, 28),
                Location = "B-66",
                IsActive = true
            },
            new()
            {
                ProductName = "USB Flash Drive - 64GB",
                Sku = "ELEC-0011",
                Category = "Electronics",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 8, 30),
                Location = "A-67",
                IsActive = true
            },
            new()
            {
                ProductName = "Mixing Bowl Set - Stainless Steel",
                Sku = "KTCH-0007",
                Category = "Kitchen",
                Price = 27.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 18),
                Location = "J-68",
                IsActive = true
            },
            new()
            {
                ProductName = "Insulated Travel Mug - 20oz",
                Sku = "KTCH-0008",
                Category = "Kitchen",
                Price = 22.99m,
                Quantity = 189,
                DateAdded = new DateTime(2025, 1, 9),
                Location = "J-69",
                IsActive = true
            },
            new()
            {
                ProductName = "Baby Monitor with Camera",
                Sku = "BABY-0001",
                Category = "Baby",
                Price = 89.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 6, 20),
                Location = "V-70",
                IsActive = false
            },
            new()
            {
                ProductName = "Cordless Vacuum Cleaner",
                Sku = "HOME-0012",
                Category = "Home",
                Price = 179.99m,
                Quantity = 18,
                DateAdded = new DateTime(2024, 11, 25),
                Location = "B-71",
                IsActive = true
            },
            new()
            {
                ProductName = "Sunglasses - Polarized",
                Sku = "ACCS-0004",
                Category = "Accessories",
                Price = 39.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 5, 10),
                Location = "Q-72",
                IsActive = true
            },
            new()
            {
                ProductName = "Dumbbell Set - 20lb Pair",
                Sku = "SPRT-0004",
                Category = "Sports",
                Price = 54.99m,
                Quantity = 45,
                DateAdded = new DateTime(2025, 3, 8),
                Location = "F-73",
                IsActive = true
            },
            new()
            {
                ProductName = "Picture Frame Set - 8x10",
                Sku = "HOME-0013",
                Category = "Home",
                Price = 29.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 7, 30),
                Location = "B-74",
                IsActive = true
            },
            new()
            {
                ProductName = "Smart LED Light Bulbs - 4 Pack",
                Sku = "ELEC-0012",
                Category = "Electronics",
                Price = 44.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 2, 12),
                Location = "A-75",
                IsActive = true
            },
            new()
            {
                ProductName = "Grilling Tools Set - 3pc",
                Sku = "OUTD-0003",
                Category = "Outdoors",
                Price = 24.99m,
                Quantity = 110,
                DateAdded = new DateTime(2024, 10, 10),
                Location = "S-76",
                IsActive = true
            },
            new()
            {
                ProductName = "Women's Crossbody Bag",
                Sku = "ACCS-0005",
                Category = "Accessories",
                Price = 49.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 12),
                Location = "Q-77",
                IsActive = false
            },
            new()
            {
                ProductName = "Desk Calendar 2025",
                Sku = "OFFC-0005",
                Category = "Office",
                Price = 9.99m,
                Quantity = 200,
                DateAdded = new DateTime(2024, 12, 1),
                Location = "K-78",
                IsActive = true
            },
            new()
            {
                ProductName = "Inflatable Pool Float",
                Sku = "TOYS-0004",
                Category = "Toys",
                Price = 19.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 4, 25),
                Location = "E-79",
                IsActive = true
            },
            new()
            {
                ProductName = "Ceramic Plant Pot - Large",
                Sku = "GRDN-0002",
                Category = "Garden",
                Price = 24.99m,
                Quantity = 135,
                DateAdded = new DateTime(2025, 1, 22),
                Location = "M-80",
                IsActive = true
            },
            new()
            {
                ProductName = "Wireless Earbuds",
                Sku = "ELEC-0013",
                Category = "Electronics",
                Price = 69.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 9, 28),
                Location = "A-81",
                IsActive = true
            },
            new()
            {
                ProductName = "Canvas Tote Bag",
                Sku = "ACCS-0006",
                Category = "Accessories",
                Price = 14.99m,
                Quantity = 240,
                DateAdded = new DateTime(2025, 3, 17),
                Location = "Q-82",
                IsActive = true
            },
            new()
            {
                ProductName = "Laundry Hamper - Collapsible",
                Sku = "HOME-0014",
                Category = "Home",
                Price = 19.99m,
                Quantity = 95,
                DateAdded = new DateTime(2024, 11, 15),
                Location = "B-83",
                IsActive = true
            },
            new()
            {
                ProductName = "Protein Bars - Variety Pack 12ct",
                Sku = "GROC-0003",
                Category = "Grocery",
                Price = 19.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 6, 8),
                Location = "C-84",
                IsActive = false
            },
            new()
            {
                ProductName = "Slow Cooker - 6 Quart",
                Sku = "KTCH-0009",
                Category = "Kitchen",
                Price = 49.99m,
                Quantity = 32,
                DateAdded = new DateTime(2025, 2, 1),
                Location = "J-85",
                IsActive = true
            },
            new()
            {
                ProductName = "Exercise Resistance Bands Set",
                Sku = "SPRT-0005",
                Category = "Sports",
                Price = 17.99m,
                Quantity = 170,
                DateAdded = new DateTime(2025, 4, 30),
                Location = "F-86",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Dress Shirt - White",
                Sku = "CLOT-0005",
                Category = "Clothing",
                Price = 34.99m,
                Quantity = 62,
                DateAdded = new DateTime(2024, 12, 20),
                Location = "D-87",
                IsActive = true
            },
            new()
            {
                ProductName = "HDMI Cable - 6ft",
                Sku = "ELEC-0014",
                Category = "Electronics",
                Price = 9.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 18),
                Location = "A-88",
                IsActive = true
            },
            new()
            {
                ProductName = "Wooden Cutting Board - Large",
                Sku = "KTCH-0010",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 118,
                DateAdded = new DateTime(2025, 5, 15),
                Location = "J-89",
                IsActive = true
            },
            new()
            {
                ProductName = "Baby Blanket - Organic Cotton",
                Sku = "BABY-0002",
                Category = "Baby",
                Price = 24.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 1, 30),
                Location = "V-90",
                IsActive = true
            },
            new()
            {
                ProductName = "Computer Monitor Stand",
                Sku = "OFFC-0006",
                Category = "Office",
                Price = 34.99m,
                Quantity = 2,
                DateAdded = new DateTime(2024, 8, 22),
                Location = "K-91",
                IsActive = false
            },
            new()
            {
                ProductName = "Outdoor String Lights - 48ft",
                Sku = "GRDN-0003",
                Category = "Garden",
                Price = 29.99m,
                Quantity = 78,
                DateAdded = new DateTime(2025, 3, 28),
                Location = "M-92",
                IsActive = true
            },
            new()
            {
                ProductName = "Shampoo - Natural Formula 16oz",
                Sku = "BEUT-0002",
                Category = "Beauty",
                Price = 12.99m,
                Quantity = 210,
                DateAdded = new DateTime(2024, 10, 18),
                Location = "U-93",
                IsActive = true
            },
            new()
            {
                ProductName = "Cat Scratching Post",
                Sku = "PETS-0003",
                Category = "Pet Supplies",
                Price = 29.99m,
                Quantity = 54,
                DateAdded = new DateTime(2025, 2, 14),
                Location = "N-94",
                IsActive = true
            },
            new()
            {
                ProductName = "Thermal Coffee Carafe - 1L",
                Sku = "KTCH-0011",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 14),
                Location = "J-95",
                IsActive = true
            },
            new()
            {
                ProductName = "Air Purifier - Small Room",
                Sku = "HOME-0015",
                Category = "Home",
                Price = 79.99m,
                Quantity = 28,
                DateAdded = new DateTime(2024, 11, 5),
                Location = "B-96",
                IsActive = true
            },
            new()
            {
                ProductName = "Children's Art Supply Kit",
                Sku = "TOYS-0005",
                Category = "Toys",
                Price = 29.99m,
                Quantity = 165,
                DateAdded = new DateTime(2025, 4, 10),
                Location = "E-97",
                IsActive = true
            },
            new()
            {
                ProductName = "Windshield Sun Shade",
                Sku = "AUTO-0003",
                Category = "Automotive",
                Price = 14.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 15),
                Location = "I-98",
                IsActive = false
            },
            new()
            {
                ProductName = "Cooling Gel Pillow",
                Sku = "HOME-0016",
                Category = "Home",
                Price = 39.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 18),
                Location = "B-99",
                IsActive = true
            },
            new()
            {
                ProductName = "Wireless Keyboard and Mouse Combo",
                Sku = "ELEC-0015",
                Category = "Electronics",
                Price = 44.99m,
                Quantity = 112,
                DateAdded = new DateTime(2025, 3, 5),
                Location = "A-00",
                IsActive = true
            },
            new()
            {
                ProductName = "Stainless Steel Food Storage Set",
                Sku = "KTCH-0012",
                Category = "Kitchen",
                Price = 32.99m,
                Quantity = 98,
                DateAdded = new DateTime(2024, 12, 10),
                Location = "J-01",
                IsActive = true
            },
            new()
            {
                ProductName = "Women's Yoga Pants",
                Sku = "CLOT-0006",
                Category = "Clothing",
                Price = 39.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 5),
                Location = "D-02",
                IsActive = true
            },
            new()
            {
                ProductName = "Magnetic Whiteboard - 24x36",
                Sku = "OFFC-0007",
                Category = "Office",
                Price = 34.99m,
                Quantity = 45,
                DateAdded = new DateTime(2025, 2, 20),
                Location = "K-03",
                IsActive = true
            },
            new()
            {
                ProductName = "Bike Repair Tool Kit",
                Sku = "SPRT-0006",
                Category = "Sports",
                Price = 24.99m,
                Quantity = 128,
                DateAdded = new DateTime(2025, 5, 22),
                Location = "F-04",
                IsActive = true
            },
            new()
            {
                ProductName = "Digital Kitchen Scale",
                Sku = "KTCH-0013",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 8),
                Location = "J-05",
                IsActive = false
            },
            new()
            {
                ProductName = "Decorative Wall Clock",
                Sku = "HOME-0017",
                Category = "Home",
                Price = 29.99m,
                Quantity = 102,
                DateAdded = new DateTime(2025, 4, 5),
                Location = "B-06",
                IsActive = true
            },
            new()
            {
                ProductName = "Travel Backpack - 25L",
                Sku = "ACCS-0007",
                Category = "Accessories",
                Price = 54.99m,
                Quantity = 58,
                DateAdded = new DateTime(2024, 11, 12),
                Location = "Q-07",
                IsActive = true
            },
            new()
            {
                ProductName = "Moisturizing Hand Cream - 3oz",
                Sku = "BEUT-0003",
                Category = "Beauty",
                Price = 8.99m,
                Quantity = 280,
                DateAdded = new DateTime(2025, 1, 25),
                Location = "U-08",
                IsActive = true
            },
            new()
            {
                ProductName = "Portable Bluetooth Speaker - Mini",
                Sku = "ELEC-0016",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 9, 1),
                Location = "A-09",
                IsActive = true
            },
            new()
            {
                ProductName = "Stainless Steel Straws - 8 Pack",
                Sku = "KTCH-0014",
                Category = "Kitchen",
                Price = 12.99m,
                Quantity = 195,
                DateAdded = new DateTime(2025, 3, 15),
                Location = "J-10",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Athletic Shorts",
                Sku = "CLOT-0007",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 115,
                DateAdded = new DateTime(2024, 12, 15),
                Location = "D-11",
                IsActive = true
            },
            new()
            {
                ProductName = "Surge Protector Power Strip - 6 Outlet",
                Sku = "ELEC-0017",
                Category = "Electronics",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 6, 25),
                Location = "A-12",
                IsActive = false
            },
            new()
            {
                ProductName = "Drawer Organizers - Set of 6",
                Sku = "HOME-0018",
                Category = "Home",
                Price = 17.99m,
                Quantity = 160,
                DateAdded = new DateTime(2025, 2, 28),
                Location = "B-13",
                IsActive = true
            },
            new()
            {
                ProductName = "Bamboo Serving Tray",
                Sku = "KTCH-0015",
                Category = "Kitchen",
                Price = 22.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 5, 8),
                Location = "J-14",
                IsActive = true
            },
            new()
            {
                ProductName = "Football - Youth Size",
                Sku = "SPRT-0007",
                Category = "Sports",
                Price = 19.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 10, 3),
                Location = "F-15",
                IsActive = true
            },
            new()
            {
                ProductName = "Wooden Toy Train Set",
                Sku = "TOYS-0006",
                Category = "Toys",
                Price = 34.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 8, 28),
                Location = "E-16",
                IsActive = true
            },
            new()
            {
                ProductName = "Glass Water Pitcher - 64oz",
                Sku = "KTCH-0016",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 138,
                DateAdded = new DateTime(2025, 1, 10),
                Location = "J-17",
                IsActive = true
            },
            new()
            {
                ProductName = "Umbrella - Compact Travel",
                Sku = "ACCS-0008",
                Category = "Accessories",
                Price = 16.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 22),
                Location = "Q-18",
                IsActive = false
            },
            new()
            {
                ProductName = "Makeup Brush Set - 12pc",
                Sku = "BEUT-0004",
                Category = "Beauty",
                Price = 29.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 12),
                Location = "U-19",
                IsActive = true
            },
            new()
            {
                ProductName = "Acrylic Photo Frame - 5x7",
                Sku = "HOME-0019",
                Category = "Home",
                Price = 14.99m,
                Quantity = 175,
                DateAdded = new DateTime(2024, 11, 28),
                Location = "B-20",
                IsActive = true
            },
            new()
            {
                ProductName = "Car Tire Pressure Gauge",
                Sku = "AUTO-0004",
                Category = "Automotive",
                Price = 9.99m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 3, 1),
                Location = "I-21",
                IsActive = true
            },
            new()
            {
                ProductName = "Multivitamin Gummies - 60ct",
                Sku = "HLTH-0005",
                Category = "Health",
                Price = 14.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 9, 10),
                Location = "H-22",
                IsActive = true
            },
            new()
            {
                ProductName = "Quilted Table Runner",
                Sku = "HOME-0020",
                Category = "Home",
                Price = 19.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 2, 6),
                Location = "B-23",
                IsActive = true
            },
            new()
            {
                ProductName = "Webcam - 1080p HD",
                Sku = "ELEC-0018",
                Category = "Electronics",
                Price = 49.99m,
                Quantity = 42,
                DateAdded = new DateTime(2024, 12, 22),
                Location = "A-24",
                IsActive = true
            },
            new()
            {
                ProductName = "Ice Cube Trays - Silicone 2pk",
                Sku = "KTCH-0017",
                Category = "Kitchen",
                Price = 11.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 30),
                Location = "J-25",
                IsActive = false
            },
            new()
            {
                ProductName = "Women's Winter Gloves",
                Sku = "ACCS-0009",
                Category = "Accessories",
                Price = 17.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 1, 8),
                Location = "Q-26",
                IsActive = true
            },
            new()
            {
                ProductName = "Jigsaw Puzzle - 1000 pieces",
                Sku = "GAME-0002",
                Category = "Games",
                Price = 19.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 5, 1),
                Location = "R-27",
                IsActive = true
            },
            new()
            {
                ProductName = "Extension Cord - 25ft Heavy Duty",
                Sku = "TOOL-0003",
                Category = "Tools",
                Price = 24.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 10, 8),
                Location = "O-28",
                IsActive = true
            },
            new()
            {
                ProductName = "Aquarium Starter Kit - 5 Gallon",
                Sku = "PETS-0004",
                Category = "Pet Supplies",
                Price = 59.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 12),
                Location = "N-29",
                IsActive = true
            },
            new()
            {
                ProductName = "Dish Drying Rack - Stainless Steel",
                Sku = "KTCH-0018",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 110,
                DateAdded = new DateTime(2025, 3, 22),
                Location = "J-30",
                IsActive = true
            },
            new()
            {
                ProductName = "Memory Foam Bath Mat",
                Sku = "HOME-0021",
                Category = "Home",
                Price = 19.99m,
                Quantity = 155,
                DateAdded = new DateTime(2024, 11, 18),
                Location = "B-31",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Polo Shirt",
                Sku = "CLOT-0008",
                Category = "Clothing",
                Price = 29.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 8, 16),
                Location = "D-32",
                IsActive = false
            },
            new()
            {
                ProductName = "Bluetooth FM Transmitter",
                Sku = "AUTO-0005",
                Category = "Automotive",
                Price = 19.99m,
                Quantity = 82,
                DateAdded = new DateTime(2025, 2, 25),
                Location = "I-33",
                IsActive = true
            },
            new()
            {
                ProductName = "Recipe Box - Wooden",
                Sku = "KTCH-0019",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 4, 28),
                Location = "J-34",
                IsActive = true
            },
            new()
            {
                ProductName = "Camping Chair - Foldable",
                Sku = "OUTD-0004",
                Category = "Outdoors",
                Price = 34.99m,
                Quantity = 55,
                DateAdded = new DateTime(2024, 9, 22),
                Location = "S-35",
                IsActive = true
            },
            new()
            {
                ProductName = "Wall-Mounted Key Holder",
                Sku = "HOME-0022",
                Category = "Home",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 6, 18),
                Location = "B-36",
                IsActive = true
            },
            new()
            {
                ProductName = "Printer Paper - 500 Sheets",
                Sku = "OFFC-0008",
                Category = "Office",
                Price = 12.99m,
                Quantity = 340,
                DateAdded = new DateTime(2025, 1, 12),
                Location = "K-37",
                IsActive = true
            },
            new()
            {
                ProductName = "Baby Teething Toys - 5 Pack",
                Sku = "BABY-0003",
                Category = "Baby",
                Price = 16.99m,
                Quantity = 198,
                DateAdded = new DateTime(2025, 5, 20),
                Location = "V-38",
                IsActive = true
            },
            new()
            {
                ProductName = "Compression Socks - 3 Pair",
                Sku = "HLTH-0006",
                Category = "Health",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 7, 28),
                Location = "H-39",
                IsActive = false
            },
            new()
            {
                ProductName = "WiFi Range Extender",
                Sku = "ELEC-0019",
                Category = "Electronics",
                Price = 39.99m,
                Quantity = 38,
                DateAdded = new DateTime(2025, 3, 10),
                Location = "A-40",
                IsActive = true
            },
            new()
            {
                ProductName = "Silicone Oven Mitts - Pair",
                Sku = "KTCH-0020",
                Category = "Kitchen",
                Price = 14.99m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 12, 3),
                Location = "J-41",
                IsActive = true
            },
            new()
            {
                ProductName = "Children's Backpack - Cartoon Design",
                Sku = "ACCS-0010",
                Category = "Accessories",
                Price = 24.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 2, 16),
                Location = "Q-42",
                IsActive = true
            },
            new()
            {
                ProductName = "Garden Pruning Shears",
                Sku = "GRDN-0004",
                Category = "Garden",
                Price = 17.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 4),
                Location = "M-43",
                IsActive = true
            },
            new()
            {
                ProductName = "Dry Erase Markers - 12 Pack",
                Sku = "OFFC-0009",
                Category = "Office",
                Price = 9.99m,
                Quantity = 275,
                DateAdded = new DateTime(2025, 4, 22),
                Location = "K-44",
                IsActive = true
            },
            new()
            {
                ProductName = "Bathtub Drain Stopper",
                Sku = "HOME-0023",
                Category = "Home",
                Price = 8.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 10, 25),
                Location = "B-45",
                IsActive = true
            },
            new()
            {
                ProductName = "Smart Watch Band - Silicone",
                Sku = "ELEC-0020",
                Category = "Electronics",
                Price = 12.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 5),
                Location = "A-46",
                IsActive = false
            },
            new()
            {
                ProductName = "Measuring Cups and Spoons Set",
                Sku = "KTCH-0021",
                Category = "Kitchen",
                Price = 14.99m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 1, 28),
                Location = "J-47",
                IsActive = true
            },
            new()
            {
                ProductName = "Basketball Hoop - Door Mount",
                Sku = "SPRT-0008",
                Category = "Sports",
                Price = 19.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 5, 12),
                Location = "F-48",
                IsActive = true
            },
            new()
            {
                ProductName = "Women's Tank Top - Pack of 3",
                Sku = "CLOT-0009",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 9, 15),
                Location = "D-49",
                IsActive = true
            },
            new()
            {
                ProductName = "Shoe Rack - 4 Tier",
                Sku = "HOME-0024",
                Category = "Home",
                Price = 29.99m,
                Quantity = 62,
                DateAdded = new DateTime(2024, 11, 20),
                Location = "B-50",
                IsActive = true
            },
            new()
            {
                ProductName = "Hand Sanitizer - 8oz Pump",
                Sku = "HLTH-0007",
                Category = "Health",
                Price = 6.99m,
                Quantity = 450,
                DateAdded = new DateTime(2025, 3, 18),
                Location = "H-51",
                IsActive = true
            },
            new()
            {
                ProductName = "LED Strip Lights - 16ft",
                Sku = "ELEC-0021",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 7, 8),
                Location = "A-52",
                IsActive = false
            },
            new()
            {
                ProductName = "Pizza Stone - 14 inch",
                Sku = "KTCH-0022",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 2, 10),
                Location = "J-53",
                IsActive = true
            },
            new()
            {
                ProductName = "Pet Hair Roller",
                Sku = "PETS-0005",
                Category = "Pet Supplies",
                Price = 11.99m,
                Quantity = 215,
                DateAdded = new DateTime(2024, 12, 8),
                Location = "N-54",
                IsActive = true
            },
            new()
            {
                ProductName = "Closet Organizer Hanging Shelves",
                Sku = "HOME-0025",
                Category = "Home",
                Price = 19.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 16),
                Location = "B-55",
                IsActive = true
            },
            new()
            {
                ProductName = "Document Shredder - Personal",
                Sku = "OFFC-0010",
                Category = "Office",
                Price = 49.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 8, 25),
                Location = "K-56",
                IsActive = true
            },
            new()
            {
                ProductName = "Car Floor Mats - Universal",
                Sku = "AUTO-0006",
                Category = "Automotive",
                Price = 29.99m,
                Quantity = 68,
                DateAdded = new DateTime(2025, 1, 16),
                Location = "I-57",
                IsActive = true
            },
            new()
            {
                ProductName = "Sewing Kit - 100 Pieces",
                Sku = "CRFT-0001",
                Category = "Crafts",
                Price = 14.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 5, 28),
                Location = "W-58",
                IsActive = true
            },
            new()
            {
                ProductName = "Nightlight - Motion Sensor",
                Sku = "HOME-0026",
                Category = "Home",
                Price = 12.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 20),
                Location = "B-59",
                IsActive = false
            },
            new()
            {
                ProductName = "Coloring Book - Adult Mandalas",
                Sku = "BOOK-0002",
                Category = "Books",
                Price = 8.99m,
                Quantity = 190,
                DateAdded = new DateTime(2025, 3, 6),
                Location = "G-60",
                IsActive = true
            },
            new()
            {
                ProductName = "Nail Clipper Set - 5 Piece",
                Sku = "BEUT-0005",
                Category = "Beauty",
                Price = 11.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 12),
                Location = "U-61",
                IsActive = true
            },
            new()
            {
                ProductName = "Spice Rack - Rotating",
                Sku = "KTCH-0023",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 78,
                DateAdded = new DateTime(2025, 2, 23),
                Location = "J-62",
                IsActive = true
            },
            new()
            {
                ProductName = "Meditation Cushion",
                Sku = "HLTH-0008",
                Category = "Health",
                Price = 34.99m,
                Quantity = 52,
                DateAdded = new DateTime(2024, 10, 12),
                Location = "H-63",
                IsActive = true
            },
            new()
            {
                ProductName = "Jump Rope - Adjustable",
                Sku = "SPRT-0009",
                Category = "Sports",
                Price = 12.99m,
                Quantity = 185,
                DateAdded = new DateTime(2025, 4, 6),
                Location = "F-64",
                IsActive = true
            },
            new()
            {
                ProductName = "Shower Curtain - Waterproof",
                Sku = "HOME-0027",
                Category = "Home",
                Price = 16.99m,
                Quantity = 112,
                DateAdded = new DateTime(2024, 11, 10),
                Location = "B-65",
                IsActive = true
            },
            new()
            {
                ProductName = "Men's Baseball Cap",
                Sku = "ACCS-0011",
                Category = "Accessories",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 7, 16),
                Location = "Q-66",
                IsActive = false
            },
            new()
            {
                ProductName = "USB-C Hub - 7 in 1",
                Sku = "ELEC-0022",
                Category = "Electronics",
                Price = 34.99m,
                Quantity = 48,
                DateAdded = new DateTime(2025, 1, 20),
                Location = "A-67",
                IsActive = true
            },
            new()
            {
                ProductName = "Cocktail Shaker Set",
                Sku = "KTCH-0024",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 5, 25),
                Location = "J-68",
                IsActive = true
            },
            new()
            {
                ProductName = "Dog Leash - Retractable",
                Sku = "PETS-0006",
                Category = "Pet Supplies",
                Price = 19.99m,
                Quantity = 138,
                DateAdded = new DateTime(2024, 12, 16),
                Location = "N-69",
                IsActive = true
            },
            new()
            {
                ProductName = "Throw Blanket - Fleece",
                Sku = "HOME-0028",
                Category = "Home",
                Price = 22.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 8, 10),
                Location = "B-70",
                IsActive = true
            },
            new()
            {
                ProductName = "Sticky Notes - Assorted Colors",
                Sku = "OFFC-0011",
                Category = "Office",
                Price = 7.99m,
                Quantity = 380,
                DateAdded = new DateTime(2025, 3, 12),
                Location = "K-71",
                IsActive = true
            },
            new()
            {
                ProductName = "Bike Water Bottle Holder",
                Sku = "SPRT-0010",
                Category = "Sports",
                Price = 9.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 22),
                Location = "F-72",
                IsActive = false
            },
            new()
            {
                ProductName = "Lint Remover - Electric",
                Sku = "HOME-0029",
                Category = "Home",
                Price = 14.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 2, 8),
                Location = "B-73",
                IsActive = true
            },
            new()
            {
                ProductName = "Reading Glasses - +2.00",
                Sku = "HLTH-0009",
                Category = "Health",
                Price = 12.99m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 10, 20),
                Location = "H-74",
                IsActive = true
            },
            new()
            {
                ProductName = "Guitar Strings - Steel",
                Sku = "MUSC-0002",
                Category = "Music",
                Price = 9.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 2),
                Location = "P-75",
                IsActive = true
            },
            new()
            {
                ProductName = "Baking Sheet Set - 3 Piece",
                Sku = "KTCH-0025",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 4, 24),
                Location = "J-76",
                IsActive = true
            },
            new()
            {
                ProductName = "Baby Wipes - 480 Count",
                Sku = "BABY-0004",
                Category = "Baby",
                Price = 16.99m,
                Quantity = 280,
                DateAdded = new DateTime(2025, 1, 6),
                Location = "V-77",
                IsActive = true
            },
            new()
            {
                ProductName = "Desktop Fan - USB Powered",
                Sku = "ELEC-0023",
                Category = "Electronics",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 8),
                Location = "A-78",
                IsActive = false
            },
            new()
            {
                ProductName = "Placemats - Set of 4",
                Sku = "KTCH-0026",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 115,
                DateAdded = new DateTime(2025, 5, 6),
                Location = "J-79",
                IsActive = true
            },
            new()
            {
                ProductName = "Adjustable Wrench - 10 inch",
                Sku = "TOOL-0004",
                Category = "Tools",
                Price = 12.99m,
                Quantity = 142,
                DateAdded = new DateTime(2024, 11, 2),
                Location = "O-80",
                IsActive = true
            },
            new()
            {
                ProductName = "Women's Cardigan Sweater",
                Sku = "CLOT-0010",
                Category = "Clothing",
                Price = 44.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 28),
                Location = "D-81",
                IsActive = true
            },
            new()
            {
                ProductName = "Bath Bombs - 6 Pack",
                Sku = "BEUT-0006",
                Category = "Beauty",
                Price = 18.99m,
                Quantity = 175,
                DateAdded = new DateTime(2025, 3, 28),
                Location = "U-82",
                IsActive = true
            },
            new()
            {
                ProductName = "Bookends - Metal Pair",
                Sku = "OFFC-0012",
                Category = "Office",
                Price = 16.99m,
                Quantity = 92,
                DateAdded = new DateTime(2024, 12, 25),
                Location = "K-83",
                IsActive = true
            },
            new()
            {
                ProductName = "Bike Lock - Cable",
                Sku = "SPRT-0011",
                Category = "Sports",
                Price = 14.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 8, 18),
                Location = "F-84",
                IsActive = false
            },
            new()
            {
                ProductName = "Corkscrew - Wine Opener",
                Sku = "KTCH-0027",
                Category = "Kitchen",
                Price = 11.99m,
                Quantity = 205,
                DateAdded = new DateTime(2025, 2, 4),
                Location = "J-85",
                IsActive = true
            },
            new()
            {
                ProductName = "Wall Hooks - 10 Pack",
                Sku = "HOME-0030",
                Category = "Home",
                Price = 8.99m,
                Quantity = 310,
                DateAdded = new DateTime(2025, 5, 18),
                Location = "B-86",
                IsActive = true
            },
            new()
            {
                ProductName = "Car Vacuum Cleaner - Handheld",
                Sku = "AUTO-0007",
                Category = "Automotive",
                Price = 34.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 9, 25),
                Location = "I-87",
                IsActive = true
            },
            new()
            {
                ProductName = "First Aid Kit - 100 Piece",
                Sku = "HLTH-0010",
                Category = "Health",
                Price = 24.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 14),
                Location = "H-88",
                IsActive = true
            },
            new()
            {
                ProductName = "Phone Screen Protector - 3 Pack",
                Sku = "ELEC-0024",
                Category = "Electronics",
                Price = 9.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 6, 8),
                Location = "A-89",
                IsActive = true
            },
            new()
            {
                ProductName = "Salad Spinner",
                Sku = "KTCH-0028",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 122,
                DateAdded = new DateTime(2025, 4, 2),
                Location = "J-90",
                IsActive = true
            }
        };

        await context.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}