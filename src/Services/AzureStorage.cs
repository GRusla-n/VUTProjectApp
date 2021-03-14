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

        
        public Task<string> EditFile(MemoryStream content, string extension, string containerName, string fileRoute)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFile(string fileRoute, string containerName)
        {
            throw new NotImplementedException();
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
