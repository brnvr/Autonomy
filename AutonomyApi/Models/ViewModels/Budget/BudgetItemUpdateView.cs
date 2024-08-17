using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetItemUpdateView
    {
        public required string Name { get; set; }
        [Range(1, 5000)]
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        [Range(1, 5000)]
        public required int Duration { get; set; }
        public required int CurrencyId { get; set; }
        public TimeUnit? DurationTimeUnit { get; set; }
    }
}