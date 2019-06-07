using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace EMPMANA.Services
{
    public interface IAzureBlobConnectionFactory
    {
        Task<CloudBlobContainer> GetBlobContainer();
    }

    public class AzureBlobConnectionFactory : IAzureBlobConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private CloudBlobContainer _CloudBlobContainer;
        private CloudBlobClient _cloudBlobClient;

        public AzureBlobConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CloudBlobContainer> GetBlobContainer()
        {
            if (_CloudBlobContainer != null)
            {
                return _CloudBlobContainer;
            }

            string blobContainerName = _configuration.GetValue<string>("BlobConnectionName");
            if (string.IsNullOrWhiteSpace(blobContainerName))
            {
                throw new ArgumentException("Configuration must contain Container Name ");
            }

            CloudBlobClient blobClient = GetClient();

            _CloudBlobContainer = blobClient.GetContainerReference(blobContainerName);
            if (await _CloudBlobContainer.CreateIfNotExistsAsync())
            {
                await _CloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return _CloudBlobContainer;
        }

        private CloudBlobClient GetClient()
        {
            if (_cloudBlobClient != null)
            {
                return _cloudBlobClient;
            }

            string storageconnectionstring = _configuration.GetValue<string>("BlobConnectionString");
            if (string.IsNullOrWhiteSpace(storageconnectionstring))
            {
                throw new ArgumentException("Configuration must contain connection string  ");
            }

            if (!CloudStorageAccount.TryParse(storageconnectionstring, out CloudStorageAccount cloudStorageAccount))
            {
                throw new Exception("Could not create Connection");
            }

            // Create a blob client for interacting with the blob service.
            _cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return _cloudBlobClient;
        }

    }
}
