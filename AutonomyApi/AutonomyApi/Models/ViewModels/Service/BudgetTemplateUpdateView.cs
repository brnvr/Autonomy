using AutonomyApi.Models.ViewModels.Budget;
using System.Reflection.Metadata;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class BudgetTemplateUpdateView
    {
        public required string Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public required BudgetItemUpdateViewList Items { get; set; }
    }
}
