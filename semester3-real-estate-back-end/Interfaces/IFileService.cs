﻿namespace semester3_real_estate_back_end.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folderName);
    Task SaveMultipleFileAsync(List<IFormFile> files, string folderName);

    void DeleteFile(string filePath, string folderName);
}