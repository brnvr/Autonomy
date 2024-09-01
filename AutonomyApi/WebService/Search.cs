using AutonomyApi.WebService.DynamicFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace AutonomyApi.WebService
{
    public class Search<T>
    {
        [SwaggerSchema("Current page")]
        public int Page { get; set; } = -1;
        [SwaggerSchema("Page length")]
        public int PageLength { get; set; }
        [SwaggerSchema("Property name to order by (optional)")]
        public string? Order { get; set; }
        [SwaggerSchema("Order direction")]
        public OrderDirection OrderDirection { get; set; }

        public SearchResults<T> GetResults(IEnumerable<T> query, int page, int pageLength)
        {
            var _query = ProcessQuery(query);

            return new SearchResults<T>(_query, Page, PageLength, query.Count());
        }

        public SearchResults<TSelected> GetResults<TSelected>(IEnumerable<T> query, Func<T, TSelected> selector)
        {
            var _query = ProcessQuery(query);

            return new SearchResults<TSelected>(_query.Select(selector), Page, PageLength, query.Count());
        }

        protected virtual DynamicFilterPipeline<T>? GetFilters()
        {
            return null;
        }

        protected IEnumerable<T> ProcessQuery(IEnumerable<T> query)
        {
            IEnumerable<T> _query = query;

            var filters = GetFilters();

            if (filters != null)
            {
                _query = filters.GetDelegate()(_query);
            }

            if (!string.IsNullOrEmpty(Order))
            {
                switch (OrderDirection)
                {
                    default:
                        _query = _query.OrderBy(Order);
                        break;

                    case OrderDirection.Descending:
                        _query = _query.OrderByDescending(Order);
                        break;
                }
            }

            if (Page >= 0)
            {
                if (PageLength < 1)
                {
                    throw new ArgumentException(nameof(PageLength), $"{nameof(PageLength)} must be an integer greater than 0 (real: {PageLength}).");
                }

                _query = _query
                    .Skip(Page * PageLength)
                    .Take(PageLength);
            }

            return _query;
        }
    }
}
