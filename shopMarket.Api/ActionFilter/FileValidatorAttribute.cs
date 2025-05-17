using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace shopMarket.Api.ActionFilter
{
    public class FileValidatorAttribute(string _allowedExtensions, int _maxFileSizeInMB = -1) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }
            if (_maxFileSizeInMB != -1)
            {
                var fileSize = file.Length;
                if (fileSize > _maxFileSizeInMB * 1024 * 1024)
                    return new ValidationResult($"File size is too large, max allowed size is {_maxFileSizeInMB} MB.");
            }
            var fileExtension = Path.GetExtension(file.FileName).TrimStart('.');

            if (!_allowedExtensions.Split(',').Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult($"Invalid file format, only [{_allowedExtensions}] are allowed.");
            }
            return ValidationResult.Success;


        }

    }
}
