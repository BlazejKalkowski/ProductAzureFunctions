using ProductAzureFunctions.API.Models;

namespace ProductAzureFunctions.API.Services
{
    public interface IProductService
    {
        Task<Guid> CreateProduct(ProductDTO dto);
    }
}
