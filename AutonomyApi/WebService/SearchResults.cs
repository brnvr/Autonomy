using AutonomyApi.WebService.DynamicFilters;
using System.Collections;
using System.Linq;

namespace AutonomyApi.WebService
{
    public class SearchResults<T>
    {
        public int TotalResults { get; }
        public IEnumerable<T> Selected { get; }
        public int Page { get; }
        public int PageLength { get; }
        public int TotalPages { get; }

        public SearchResults(IEnumerable<T> selected, int page, int pageLength, int totalResults)
        {
            TotalResults = totalResults;
            Selected = selected;
            Page = Math.Max(page, 0);
            PageLength = pageLength;
            TotalPages = pageLength == 0 ? 1 : (totalResults / pageLength + 1);
        }
    }
}
