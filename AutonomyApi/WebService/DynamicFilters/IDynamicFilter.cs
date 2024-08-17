namespace AutonomyApi.WebService.DynamicFilters
{
    public interface IDynamicFilter<T>
    {
        bool IsMatch(T entity);
    }
}
