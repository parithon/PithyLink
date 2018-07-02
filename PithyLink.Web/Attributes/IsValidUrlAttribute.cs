using System;
using System.ComponentModel.DataAnnotations;

namespace PithyLink.Web.Attributes
{
    public class IsValidUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is string == false
                ? new ValidationResult($"Unknown property: {value}")
                : !Uri.TryCreate(value as string, UriKind.Absolute, out Uri result)
                ? new ValidationResult("Not a vadid url.")
                : result.Scheme != "http" && result.Scheme != "https"
                ? new ValidationResult("Only a valid HTTP or HTTPS url is supported at this time.")
                : ValidationResult.Success;
        }
    }
}