namespace AutonomyApi.WebService.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message, Exception? innerException = null) : base(message, innerException) { }

        public EntityNotFoundException(Type entityType, Exception? innerException = null) : base
        (
             $"Entity of type {entityType.Name} not found.", innerException
        ) { }

        public EntityNotFoundException(Type entityType, params object?[] keyValues) : base
        (
             $"Unable to find entity of type {entityType.Name} with key value(s) [{string.Join(',', keyValues)}]."
        ) { }

        public EntityNotFoundException(Type entityType, object?[] keyValues, Exception? innerException = null) : base
        (
             $"Unable to find entity of type {entityType.Name} with key value(s) [{string.Join(',', keyValues)}].", innerException
        ) { }
    }
}
