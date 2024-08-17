using System.Collections.ObjectModel;

namespace AutonomyApi.WebService.DynamicFilters
{
    public class DynamicFilterPipeline<T> : Collection<IDynamicFilter<T>>
    {
        public bool MatchAll(T entity)
        {
            foreach (var filter in this)
            {
                if (!filter.IsMatch(entity))
                {
                    return false;
                }
            }

            return true;
        }

        public DynamicFilterPipelineDelegate<T> GetDelegate()
        {
            return e =>
            {
                return e.Where(entity => MatchAll(entity));
            };
        }
    }
}
