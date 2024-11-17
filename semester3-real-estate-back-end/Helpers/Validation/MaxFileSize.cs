using System.ComponentModel.DataAnnotations;

namespace semester4.Helpers.Validation;

public class MaxFileSize : ValidationAttribute
{
    private readonly int _maxFileSize;

    /*Mặc định là KB*/
    public MaxFileSize(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
            if (file.Length > _maxFileSize)
                return new ValidationResult(GetErrorMessage());

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.";
    }
}