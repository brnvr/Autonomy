namespace AutonomyApi.WebService.Validators
{
    public interface IValidator<TValue>
    {
        TValue Value { get; }
        public bool IsValid(out string errorMessage);
    }
}
