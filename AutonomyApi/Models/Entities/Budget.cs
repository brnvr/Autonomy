namespace AutonomyApi.Models.Entities
{
    public class Budget
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public required List<BudgetItem> Items { get; set; }
        public required bool IsTemplate { internal get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
