using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attributes
{
    public class AllowedMimeTypesAttribute : ValidationAttribute
    {
        private readonly string[] _mimeTypes;

        public AllowedMimeTypesAttribute(string[] mimeTypes)
        {
            _mimeTypes = mimeTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var mimeType = file.ContentType;
                if (!_mimeTypes.Contains(mimeType))
                {
                    return new ValidationResult($"This file type ({mimeType}) is not allowed!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
