using AutonomyApi.Enums;
using AutonomyApi.Validators;
using AutonomyApi.WebService.Validators;
using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.ValidationAttributes
{
    public class DocumentAttribute : ValidationAttribute
    {
        string _typePropertyName;

        static ValidationMap<DocumentType, string> DocumentTypeValidationMap = new ValidationMap<DocumentType, string>
        {
            { DocumentType.Cpf, value => new CpfValidator(value) }
        }.Freeze();

        public DocumentAttribute(string typePropertyName)
        {
            _typePropertyName = typePropertyName;
        }
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be a non-null object.");
            }

            DocumentType type;

            try
            {
                type = GetDocumentType(validationContext);
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }

            var isValid = DocumentTypeValidationMap.IsValid(type, value.ToString() ?? "", out string errorMessage);

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(errorMessage);
        }

        DocumentType GetDocumentType(ValidationContext validationContext)
        {
            var typeProperty = validationContext.ObjectType.GetProperty(_typePropertyName);

            if (typeProperty == null)
            {
                throw new ArgumentNullException(_typePropertyName, $"Property with name {_typePropertyName} not found.");
            }

            var typePropertyValue = typeProperty.GetValue(validationContext.ObjectInstance);

            if (!(typePropertyValue is DocumentType))
            {
                throw new Exception($"{_typePropertyName} must be a {nameof(DocumentType)}.");
            }

            return (DocumentType)typePropertyValue;
        }
    }
}