using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProductManagementSystem.IntegrationTests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/products")]
    [InlineData("/products/create")]
    [InlineData("/products/details/1")]
    [InlineData("/products/edit/1")]
    [InlineData("/products/delete/1")]
    [InlineData("/employees")]
    [InlineData("/employees/create")]
    [InlineData("/employees/edit/1")]
    [InlineData("/employees/delete/1")]
    public async Task Get_KnownEndpoints_ReturnSuccessStatusCode(string path)
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(path);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task Get_UnknownRoute_Returns404()
    {
        // Arrange
        using var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }
        );

        // Act
        var response = await client.GetAsync("/this-route-should-not-exist-12345");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Get_Root_ReturnsHtml()
    {
        // Arrange
        using var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = true }
        );

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();

        var contentType = response.Content.Headers.ContentType;
        Assert.NotNull(contentType);
        Assert.Equal("text/html", contentType!.MediaType);

        var html = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(html));
    }
}
