using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetSearchView : Search<Entities.Budget>
    {
        [SwaggerSchema("Budget name filter (optional)")]
        public string? Name { get; set; }

        protected override DynamicFilterPipeline<Entities.Budget>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.Budget>
            {
                new SubstringFilter<Entities.Budget>(Name, budget => budget.Name)
            };
        }
    }
}
