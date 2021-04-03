using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace VUTProjectApp.Services
{
    public class AzureStorage : IFileStorage
    {        
        private readonly string connectionString;

        public AzureStorage(IConfiguration configuration)
        {            
            connectionString = configuration.GetConnectionString("AzuraStorage");
        }       

        
        public async Task<string> EditFile(MemoryStream content, string extension, string containerName, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(content, extension, containerName);

        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            var blobName = Path.GetFileName(fileRoute);
            var blob = container.GetBlobClient(blobName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> SaveFile(MemoryStream content, string extension, string containerName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            string fileName = $"{Guid.NewGuid()}{extension}";
            var blob = container.GetBlobClient(fileName);
            content.Position = 0;
            await blob.UploadAsync(content);
            return blob.Uri.ToString();
        }
    }
}
