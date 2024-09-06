using AutonomyApi.Models.ViewModels.Budget;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class BudgetTemplateUpdateView
    {
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public int CurrencyId { get; set; }
        public required BudgetItemUpdateViewList Items { get; set; }
    }
}
