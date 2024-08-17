using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetItemPresentationView
    {
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public required int Duration { get; set; }
        public TimeUnit? DurationTimeUnit { get; set; }
        public Currency? Currency { get; set; }
        public decimal Total { get => Quantity * Duration * UnitPrice; }
    }
}