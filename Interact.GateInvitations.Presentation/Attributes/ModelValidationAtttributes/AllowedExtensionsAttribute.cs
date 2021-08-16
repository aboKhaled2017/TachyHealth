using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Attributes.ModelValidationAtttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly UploadedType _uploadType;
        public AllowedExtensionsAttribute(string[] extensions, UploadedType uploadType = UploadedType.Single)
        {
            _extensions = extensions;
            _uploadType = uploadType;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            bool validateFile(IFormFile _file)
            {
                var extension = Path.GetExtension(_file.FileName).Replace(".", "");
                if (_file != null)
                {
                    return (_extensions.Contains(extension.ToLower()));
                }
                return true;
            }
            if (value == null) return null;
            if(_uploadType==UploadedType.Single)
            {
                var file = value as IFormFile;
                if (!validateFile(file))
                {
                  return  new ValidationResult(GetErrorMessage());
                }
            }
            else
            {
                var files = value as IList<IFormFile>;
                if (files.Select(e => validateFile(e)).Any(e => !e))
                {
                   return  new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This file extension is not supported";
        }
        public enum UploadedType
        {
            Single=0,
            Multiple=1
        }
    }
}
