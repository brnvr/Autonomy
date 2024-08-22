using AutonomyApi.WebService.DynamicFilters;
using System.Collections;
using System.Linq;

namespace AutonomyApi.WebService
{
    public class SearchResults<T>
    {
        public int TotalResults { get; }
        public IEnumerable<T> ResultsFiltered { get; }

        public SearchResults(IEnumerable<T> resultsFiltered, int totalResults)
        {
            TotalResults = totalResults;
            ResultsFiltered = resultsFiltered;
        }
    }
}
