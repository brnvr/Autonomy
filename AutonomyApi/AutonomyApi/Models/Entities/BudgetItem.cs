using AutonomyApi.Enums;

namespace AutonomyApi.Models.Entities
{
    public class BudgetItem
    {
        internal int BudgetId { get; set; }
        public int Id { get; private set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public required int Duration { get; set; }
        public TimeUnit? DurationTimeUnit { get; set; }
    }
}
