using ProductAzureFunctions.Functions.Entity;
using System.Threading.Tasks;

namespace ProductAzureFunctions.Functions.Services
{
    public interface ITableStorageService
    {
        Task AddProductAsync(ProductEntity product);
    }
}
