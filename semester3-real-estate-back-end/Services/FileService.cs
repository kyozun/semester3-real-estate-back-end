using semester4.Interfaces;

namespace semester3_real_estate_back_end.Services;

public class FileService : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile file, string folderName)
    {
        var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

        // Nếu chưa có thư mục chứa file thì tạo mới
        if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);

        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);

        // Tạo đường dẫn đến file
        var filePathToDatabase = Guid.NewGuid() + fileExtension;
        var filePath = Path.Combine(uploadFolderPath, filePathToDatabase);

        // Kiểm tra xem file đã tồn tại chưa
        if (!File.Exists(filePath))
            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


        return filePathToDatabase;
    }

    public async Task SaveMultipleFileAsync(List<IFormFile> files, string folderName)
    {
        foreach (var file in files)
        {
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // Nếu chưa có thư mục chứa file thì tạo mới
            if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);

            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            // Tạo đường dẫn đến file
            var filePath = Path.Combine(uploadFolderPath, Guid.NewGuid() + fileExtension);

            // Kiểm tra xem file đã tồn tại chưa
            if (!File.Exists(filePath))
                // Lưu file vào thư mục
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
        }
    }

    public void DeleteFile(string filePath, string folderName)
    {
        var fullPath = Path.Combine("wwwroot", folderName, filePath);
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}