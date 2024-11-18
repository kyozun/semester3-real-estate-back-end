using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.Wrapper;
using semester4.Interfaces;

namespace semester4.Controllers;

[ApiController]
[Route("api/blob")]
public class BlobController : ControllerBase
{
    private const long MaxFileSize = 2 * 1024 * 1024; // 2MB in bytes
    private readonly IBlobStorageService _blobStorageService;

    public BlobController(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(IFormFile? file)
    {
        if (file == null || file.Length == 0) return BadRequest("File is empty");

        const string containerName = "avatar";
        var result = await _blobStorageService.UploadFileAsync(file, containerName);

        return Ok(new { FileUrl = result });
    }

    [HttpPost]
    [Route("upload-multiple")]
    public async Task<IActionResult> UploadMultipleFiles([FromForm] List<IFormFile>? files)
    {
        if (files == null || files.Count == 0) return BadRequest(new BadRequestResponse("No files provided"));

        // Check file sizes
        foreach (var file in files)
            if (file.Length > MaxFileSize)
                return BadRequest($"File {file.FileName} exceeds the 2MB size limit");

        const string containerName = "manga";
        var result = await _blobStorageService.UploadMultipleFiles(files, containerName);

        return Ok(new { FileUrls = result });
    }
}