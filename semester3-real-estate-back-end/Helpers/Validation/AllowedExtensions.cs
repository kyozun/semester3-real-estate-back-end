using System.ComponentModel.DataAnnotations;

namespace semester4.Helpers.Validation;

public class AllowedExtensions : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensions(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower())) return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return "File extension is not allowed";
    }
}