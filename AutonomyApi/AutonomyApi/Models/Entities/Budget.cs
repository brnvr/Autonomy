namespace AutonomyApi.Models.Entities
{
    public class Budget
    {
        public int Id { get; private set; }
        public required int UserId { internal get; set; }
        public required string Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public required List<BudgetItem> Items { get; set; }
    }
}
