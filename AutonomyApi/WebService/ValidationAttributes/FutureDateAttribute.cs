using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.WebService.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = value as DateTime?;

            if (date is null)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a valid DateTime.");
            }
            DateTime lol;

            if (date < DateTime.UtcNow)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a DateTime greater than current date (real: {((DateTime)date).ToUniversalTime()}).");
            }

            return ValidationResult.Success;
        }
    }
}
