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
             $"Entity of type {entityType.Name} not found for key values [{string.Join(',', keyValues)}]."
        )
        { }

        public EntityNotFoundException(Type entityType, Exception innerException, params object?[] keyValues) : base
        (
             $"Entity of type {entityType.Name} not found for key values [{string.Join(',', keyValues)}].", innerException
        ) { }
    }
}
