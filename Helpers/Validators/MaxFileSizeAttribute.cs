using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Helpers.Validators;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success;
    }
}