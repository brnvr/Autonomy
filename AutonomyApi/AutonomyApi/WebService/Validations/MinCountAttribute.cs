using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.WebService.Validations
{
    public class MinCountAttribute : ValidationAttribute
    {
        public int MinItems { get; set; }

        public MinCountAttribute(int minItems)
        {
            MinItems = minItems;
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

            if (collection.Count < MinItems)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be a non-null collection with more than {MinItems} item(s) (real: {collection.Count}).");
            }

            return ValidationResult.Success;
        }
    }
}