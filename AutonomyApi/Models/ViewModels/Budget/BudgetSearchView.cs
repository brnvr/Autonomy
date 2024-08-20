using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetSearchView : Search<Entities.Budget>
    {
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
