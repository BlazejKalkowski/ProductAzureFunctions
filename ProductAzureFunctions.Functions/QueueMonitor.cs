using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductAzureFunctions.Functions.Entity;
using ProductAzureFunctions.Functions.Services;

namespace ProductAzureFunctions.Functions
{
    public class QueueMonitor
    {
        private readonly List<string> _tables = new List<string>() { "FinancialProduct", "ShippingProduct" };
        private readonly string _storageUri = Environment.GetEnvironmentVariable("AzureTableUri");
        private readonly string _storageKey = Environment.GetEnvironmentVariable("AzureStorageKey");
        private readonly string _storageName = Environment.GetEnvironmentVariable("AzureStorageName");

        [FunctionName("QueueMonitor")]
        public async Task Run([QueueTrigger("productqueue", Connection = "ProductStorage")] string myQueueItem, ILogger log)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(myQueueItem);

                foreach (var item in _tables)
                {
                    var newProduct = new ProductEntity
                    {
                        PartitionKey = item,
                        RowKey = Guid.NewGuid().ToString(),
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        CreatedOn = product.CreatedOn.ToUniversalTime()
                    };
                    var tableService = new TableStorageService(_storageUri, _storageName, _storageKey, item, log);
                    await tableService.AddProductAsync(newProduct);
                }
            }
            catch (Exception ex)
            {

                log.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
