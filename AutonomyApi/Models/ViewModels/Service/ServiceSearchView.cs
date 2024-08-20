using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class ServiceSearchView : Search<Entities.Service>
    {
        public string? NameOrDescription { get; set; }

        protected override DynamicFilterPipeline<Entities.Service>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.Service>
            {
                new SubstringFilter<Entities.Service>(NameOrDescription, service => [ service.Name, service.Description ])
            };
        }
    }
}
