namespace AutonomyApi.WebService.DynamicFilters
{
    public delegate IEnumerable<T> DynamicFilterPipelineDelegate<T>(IEnumerable<T> conditions);
}
