using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Models.ViewModels.Schedule
{
    public class ScheduleSearchView : Search<Entities.Schedule>
    {
        public string? NameOrDescription { get; set; }
        public int? ClientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        protected override DynamicFilterPipeline<Entities.Schedule>? GetFilters()
        {
            return new DynamicFilterPipeline<Entities.Schedule>
            {
                new SubstringFilter<Entities.Schedule>(NameOrDescription, schedule => [ schedule.Name, schedule.Description ]),

                ComparisonFilter<Entities.Schedule, int?>.Equals(ClientId, schedule =>
                {
                    return schedule.Clients.Select(client => (int?)client.Id);
                }),

                ComparisonFilter<Entities.Schedule, DateTime?>.LessThanOrEqualTo(StartDate, schedule => [ schedule.Date ]),
                ComparisonFilter<Entities.Schedule, DateTime?>.GreaterThanOrEqualTo(EndDate, schedule => [ schedule.Date ])
            };
        }
    }
}
