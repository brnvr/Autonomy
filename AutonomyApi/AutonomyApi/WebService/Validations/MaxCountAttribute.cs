using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.WebService.Validations
{
    public class MaxCountAttribute : ValidationAttribute
    {
        public int MaxItems { get; set; }

        public MaxCountAttribute(int maxItems)
        {
            MaxItems = maxItems;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a non-null collection.");
            }

            var collection = value as ICollection;

            if (collection == null)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a non-null collection.");
            }

            if (collection.Count > MaxItems)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a non-null collection with less than {MaxItems} item(s) (real: {collection.Count}).");
            }

            return ValidationResult.Success;
        }
    }
}