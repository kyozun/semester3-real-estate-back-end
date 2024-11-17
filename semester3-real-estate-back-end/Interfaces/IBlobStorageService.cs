using Microsoft.AspNetCore.Mvc;

namespace semester4.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadFileAsync(IFormFile? file, string containerName);
    Task<List<string>> UploadMultipleFiles([FromForm] List<IFormFile> files, string containerName);
}