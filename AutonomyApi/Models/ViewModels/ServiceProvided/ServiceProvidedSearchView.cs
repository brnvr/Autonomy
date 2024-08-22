using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace AutonomyApi.Models.ViewModels.ServiceProvided
{
    public class ServiceProvidedSearchView : Search<Entities.ServiceProvided>
    {
        [SwaggerSchema("Service id (optional)")]
        public int? ServiceId { get; set; }
        [SwaggerSchema("Client id (optional)")]
        public int? ClientId { get; set; }
        [SwaggerSchema("Start date (optional)")]
        public DateTime? StartDate { get; set; }
        [SwaggerSchema("End date (optional)")]
        public DateTime? EndDate { get; set; }

        protected override DynamicFilterPipeline<Entities.ServiceProvided>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.ServiceProvided>
            {
                ExpFilter<Entities.ServiceProvided, int?>
                    .Equals(ServiceId, service => [ service.ServiceId ]),

                ExpFilter<Entities.ServiceProvided, int?>
                    .Equals(ClientId, service => service.Clients.Select(client => client.ClientId)),

                ExpFilter<Entities.ServiceProvided, DateTime?>
                    .LessThanOrEqualTo(StartDate, service => [ service.Date ]),

                ExpFilter<Entities.ServiceProvided, DateTime?>
                    .GreaterThanOrEqualTo(EndDate, service => [ service.Date ])
            };
        }
    }
}
