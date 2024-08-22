using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace AutonomyApi.Models.ViewModels.Schedule
{
    public class ScheduleSearchView : Search<Entities.Schedule>
    {
        [SwaggerSchema("Schedule name or description filter (optional)")]
        public string? NameOrDescription { get; set; }
        [SwaggerSchema("Client id (optional)")]
        public int? ClientId { get; set; }
        [SwaggerSchema("Start date (optional)")]
        public DateTime? StartDate { get; set; }
        [SwaggerSchema("End date (optional)")]
        public DateTime? EndDate { get; set; }

        protected override DynamicFilterPipeline<Entities.Schedule>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.Schedule>
            {
                new SubstringFilter<Entities.Schedule>(NameOrDescription, schedule => [ schedule.Name, schedule.Description ]),

                ExpFilter<Entities.Schedule, int?>.Equals(ClientId, schedule =>
                {
                    return schedule.Clients.Select(client => (int?)client.Id);
                }),

                ExpFilter<Entities.Schedule, DateTime?>.LessThanOrEqualTo(StartDate, schedule => [ schedule.Date ]),
                ExpFilter<Entities.Schedule, DateTime?>.GreaterThanOrEqualTo(EndDate, schedule => [ schedule.Date ])
            };
        }
    }
}
