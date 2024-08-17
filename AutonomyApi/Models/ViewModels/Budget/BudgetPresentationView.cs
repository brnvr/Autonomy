namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetPresentationView
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public required List<BudgetItemPresentationView> Items { get; set; }
    }
}