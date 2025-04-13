using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace LostAndFound.Helpers.Validators;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ValidateComplexTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // If value is null, delegate null-check to another attribute (like [Required])
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var results = new List<ValidationResult>();

            // If value is an IEnumerable (but not a string), then iterate through the elements.
            if (value is IEnumerable enumerable && !(value is string))
            {
                int index = 0;
                foreach (var item in enumerable)
                {
                    if (item != null)
                    {
                        var context = new ValidationContext(item, null, null);
                        // Validate the object and accumulate all errors.
                        Validator.TryValidateObject(item, context, results, validateAllProperties: true);
                    }
                    index++;
                }
            }
            else
            {
                // Otherwise, validate the single object.
                var context = new ValidationContext(value, null, null);
                Validator.TryValidateObject(value, context, results, validateAllProperties: true);
            }

            if (results.Any())
            {
                // Here we choose to combine errors into one message.
                // You can modify this to return more granular error results if needed.
                return new ValidationResult(string.Join("; ", results.Select(r => r.ErrorMessage)));
            }

            return ValidationResult.Success;
        }
    }

