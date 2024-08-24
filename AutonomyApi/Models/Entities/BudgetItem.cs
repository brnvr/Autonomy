using AutonomyApi.Enums;

namespace AutonomyApi.Models.Entities
{
    public class BudgetItem
    {
        public int BudgetId { internal get; set; }
        public required int Position { get; set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public Currency? Currency { get; set; }
        public required int CurrencyId { get; set; }
        public int Duration { get; set; }
        public TimeUnit? DurationTimeUnit { get; set; }

        public decimal GetTotal()
        {
            return Quantity * UnitPrice * Duration;
        }
    }
}
