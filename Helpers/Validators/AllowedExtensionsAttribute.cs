using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Helpers.Validators;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success;
    }
}