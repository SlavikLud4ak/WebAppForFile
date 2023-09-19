using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebAppForFile.Options;
using WebAppForFile.Services.Abstract;

namespace WebAppForFile.Services.Concrete
{
    public class FileServices : IFileServices
    {
        private readonly AzureOption _azureOptions;

        public FileServices(IOptions<AzureOption> azureOptions) { 
            _azureOptions = azureOptions.Value;
        }
        public void UploadFileToAzure(IFormFile file) {            

            string fileExtension = Path.GetExtension(file.FileName);

            using MemoryStream fileUploadStream = new MemoryStream();
            file.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;

            BlobContainerClient blobContainerClient = new BlobContainerClient(_azureOptions.ConnectionSring, _azureOptions.Container);
            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                }
            }, cancellationToken: default);            

        }
    }
}
