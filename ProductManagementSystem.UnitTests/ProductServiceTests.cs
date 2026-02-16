using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementSystem.DTO.Products;
using ProductManagementSystem.Services.Products;
using ProductManagementSystem.UnitTests.TestHelpers;

namespace ProductManagementSystem.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<ILogger<ProductService>> _loggerMock;

    public ProductServiceTests()
    {
        _loggerMock = new Mock<ILogger<ProductService>>();
    }

    [Fact]
    public async Task AddProductAsync_IfNull_ReturnsArgumentNullException()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);
        ProductAddRequest? product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await sut.AddProductAsync(product);
        });
    }

    [Fact]
    public async Task AddProductAsync_IfNotNull_PersistsToDb()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);
        var product = new ProductAddRequest
        {
            ProductName = "Test Product",
            Category = "Test Category",
            Price = 19.99m,
            Quantity = 20,
            DateAdded = DateTime.Now.AddDays(-2),
            IsActive = false,
        };

        // Act
        await sut.AddProductAsync(product);

        // Assert
        var productCount = context.Products.Count();
        Assert.Equal(4, productCount);

        var savedProduct = context.Products.FirstOrDefault(p => p.ProductName == "Test Product");
        Assert.NotNull(savedProduct);
        Assert.Equal(product.ProductName, savedProduct.ProductName);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsSeededProducts()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var products = await sut.GetAllProductsAsync();

        // Assert
        Assert.Equal(3, products.Count);
        Assert.Equal("Keyboard", products[0].ProductName);
    }

    [Fact]
    public async Task GetProductAsync_IfIdNull_ReturnsNull()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var result = await sut.GetProductAsync(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetProductAsync_IfIdNotFound_ReturnsNull()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var result = await sut.GetProductAsync(9999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetProductAsync_IfIdExists_ReturnsProduct()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var result = await sut.GetProductAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result!.Id);
        Assert.Equal("Keyboard", result.ProductName);
    }

    [Fact]
    public async Task UpdateProductAsync_IfNull_ThrowsArgumentNullException()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);
        ProductUpdateRequest? updateRequest = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await sut.UpdateProductAsync(updateRequest);
        });
    }

    [Fact]
    public async Task UpdateProductAsync_IfIdDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        var updateRequest = new ProductUpdateRequest
        {
            Id = 9999,
            ProductName = "Does not matter",
            Category = "Does not matter",
            Price = 1.23m,
            Quantity = 1,
            DateAdded = DateTime.Now,
            Location = "A001",
            IsActive = true,
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await sut.UpdateProductAsync(updateRequest);
        });
    }

    [Fact]
    public async Task UpdateProductAsync_IfValid_UpdatesPersistedEntity()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        var updateRequest = new ProductUpdateRequest
        {
            Id = 2,
            ProductName = "Mouse (Updated)",
            Sku = "SKU-UPDATED",
            Category = "Hardware",
            Price = 99.99m,
            Quantity = 42,
            DateAdded = DateTime.Now.AddDays(-10),
            Location = "B002",
            IsActive = false,
        };

        // Act
        var updated = await sut.UpdateProductAsync(updateRequest);

        // Assert (returned DTO)
        Assert.Equal(2, updated.Id);
        Assert.Equal("Mouse (Updated)", updated.ProductName);

        // Assert (database state)
        var entity = context.Products.First(p => p.Id == 2);
        Assert.Equal("Mouse (Updated)", entity.ProductName);
        Assert.Equal("SKU-UPDATED", entity.Sku);
        Assert.Equal(99.99m, entity.Price);
        Assert.Equal(42, entity.Quantity);
        Assert.Equal("B002", entity.Location);
        Assert.False(entity.IsActive);
    }

    [Fact]
    public async Task DeleteProductAsync_IfIdNull_ThrowsArgumentNullException()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await sut.DeleteProductAsync(null);
        });
    }

    [Fact]
    public async Task DeleteProductAsync_IfIdNotFound_ReturnsFalse()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var result = await sut.DeleteProductAsync(9999);

        // Assert
        Assert.False(result);
        Assert.Equal(3, context.Products.Count());
    }

    [Fact]
    public async Task DeleteProductAsync_IfIdExists_RemovesAndReturnsTrue()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var result = await sut.DeleteProductAsync(3);

        // Assert
        Assert.True(result);
        Assert.Equal(2, context.Products.Count());
        Assert.Null(context.Products.FirstOrDefault(p => p.Id == 3));
    }
}
