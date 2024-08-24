using AutonomyApi.WebService.DynamicFilters;
using System.Collections;
using System.Linq;

namespace AutonomyApi.WebService
{
    public class SearchResults<T>
    {
        public int TotalResults { get; }
        public IEnumerable<T> Selected { get; }

        public SearchResults(IEnumerable<T> selected, int totalResults)
        {
            TotalResults = totalResults;
            Selected = selected;
        }
    }
}
