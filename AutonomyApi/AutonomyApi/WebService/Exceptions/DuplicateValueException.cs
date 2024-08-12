namespace AutonomyApi.WebService.Exceptions
{
    public class DuplicateValueException : Exception
    {
        public DuplicateValueException(string message, Exception? innerException = null) : base(message, innerException) { }
        
        public DuplicateValueException(Type entityType, string property, object value, Exception? innerException = null) : base
        (
            $"Duplicate value for property {property} of entity {entityType.Name} ({value}).", innerException
        ) { }
    }
}
