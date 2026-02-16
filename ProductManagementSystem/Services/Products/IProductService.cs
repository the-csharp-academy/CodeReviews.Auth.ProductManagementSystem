using ProductManagementSystem.DTO.Products;

namespace ProductManagementSystem.Services.Products;

public interface IProductService
{
    public Task<ProductResponse> AddProductAsync(ProductAddRequest? addRequest);

    public Task<List<ProductResponse>> GetAllProductsAsync();

    public Task<ProductResponse?> GetProductAsync(int? id);

    public Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest? updateRequest);

    public Task<bool> DeleteProductAsync(int? id);
}