using Azure.Data.Tables;
using Azure;
using Microsoft.Extensions.Logging;
using ProductAzureFunctions.Functions.Entity;
using System;
using System.Threading.Tasks;

namespace ProductAzureFunctions.Functions.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly TableClient _tableClient;
        private readonly ILogger _logger;

        public TableStorageService(string storageUri, string accountName, string accountKey, string tableName, ILogger log)
        {
            var serviceClient = new TableServiceClient(
                new Uri(storageUri),
                new TableSharedKeyCredential(accountName, accountKey));
                _tableClient = serviceClient.GetTableClient(tableName);
                _logger = log;
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            try
            {
                await _tableClient.AddEntityAsync(product);
                _logger.LogInformation("Entity added to table");
            }
            catch (RequestFailedException ex)
            {
                _logger.LogWarning(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
