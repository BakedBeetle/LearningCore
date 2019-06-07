using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EMPMANA.Services
{
    public interface IAzureBlobService
    {
        Task<IEnumerable<Uri>> ListAsync();

        Task UploadAsync(IFormFileCollection formFiles);

        Task DeleteAsync(string fileUri);

        Task DeleteAllAsync();
    }

    public class AzureBlobService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory azureBlobConnectionFactory;

        public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {
            this.azureBlobConnectionFactory = azureBlobConnectionFactory;
        }
   

        public async Task<IEnumerable<Uri>> ListAsync()
        {
            var blobContainer = await azureBlobConnectionFactory.GetBlobContainer();
            var  allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                BlobResultSegment response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        allBlobs.Add(blob.Uri);
                    }
                }
                blobContinuationToken = response.ContinuationToken;

            } while (blobContinuationToken != null);
            return allBlobs;
        }

        public async Task UploadAsync(IFormFileCollection formFiles)
        {
            var blobContainer = await azureBlobConnectionFactory.GetBlobContainer();
            for (int i = 0; i < formFiles.Count; i++)
            {
                var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(formFiles[i].FileName));

                using (Stream stream = formFiles[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);

                }
            }
        }

        public async Task DeleteAsync(string fileUri)
        {
            var blobContainer = await azureBlobConnectionFactory.GetBlobContainer();
            Uri uri = new Uri(fileUri);
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task DeleteAllAsync()
        {
            var blobContainer = await azureBlobConnectionFactory.GetBlobContainer();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                BlobResultSegment response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        await((CloudBlockBlob)blob).DeleteIfExistsAsync();
                    }
                }
                blobContinuationToken = response.ContinuationToken;

            } while (blobContinuationToken != null);
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
