using Azure.Storage.Queues;
using ProductAzureFunctions.API.Entity;
using ProductAzureFunctions.API.Models;
using System.Text.Json;
using System.Text;

namespace ProductAzureFunctions.API.Services
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _queueClient = new QueueClient(_configuration["AzureSA:ConnectionString"], "productqueue");
        }

        public async Task<Guid> CreateProduct(ProductDTO dto)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedOn = DateTime.UtcNow.ToUniversalTime()
            };

            var productString = JsonSerializer.Serialize(product);
            var base64Product = Convert.ToBase64String(Encoding.UTF8.GetBytes(productString));

            await _queueClient.SendMessageAsync(base64Product);

            return product.Id;
        }
    }
}
