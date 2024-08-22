using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace AutonomyApi.Models.ViewModels.Client
{
    public class ClientSearchView : Search<Entities.Client>
    {
        [SwaggerSchema("Client name filter (optional)")]
        public string? Name { get; set; }

        protected override DynamicFilterPipeline<Entities.Client>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.Client>
            {
                new SubstringFilter<Entities.Client>(Name, client => client.Name)
            };
        }
    }
}
