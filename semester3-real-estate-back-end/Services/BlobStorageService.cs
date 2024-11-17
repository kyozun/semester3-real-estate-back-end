using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using semester4.Interfaces;

namespace semester3_real_estate_back_end.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadFileAsync(IFormFile? file, string containerName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        // Generate a unique file name based on the current time
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        var fileExtension = Path.GetExtension(file!.FileName); // Get the file extension
        var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{timestamp}{fileExtension}";

        var blobClient = blobContainerClient.GetBlobClient(newFileName);

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        var url = blobClient.Uri.ToString();

        return url;
    }


    public async Task<List<string>> UploadMultipleFiles(List<IFormFile> files, string containerName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var fileUrls = new List<string>();

        foreach (var file in files)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            var fileExtension = Path.GetExtension(file.FileName);

            // Remove white spaces from the file name
            var fileNameWithoutWhiteSpaces = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "");

            var newFileName = $"{fileNameWithoutWhiteSpaces}-{timestamp}{fileExtension}";

            var blobClient = blobContainerClient.GetBlobClient(newFileName);

            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            fileUrls.Add(blobClient.Uri.ToString());
        }

        return fileUrls;
    }
}