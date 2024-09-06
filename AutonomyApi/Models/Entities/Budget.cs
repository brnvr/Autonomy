namespace AutonomyApi.Models.Entities
{
    public class Budget
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }
        public Currency? Currency { get; set; }
        public required int CurrencyId { get; set; }
        public required List<BudgetItem> Items { get; set; }
        public required bool IsTemplate { internal get; set; }
        public required DateTime CreationDate { get; set; }

        public decimal GetTotal()
        {
            return Items.Sum(item => item.GetTotal());
        }

        public Budget Copy(bool isTemplate = false, DateTime? creationDate = null)
        {
            return new Budget
            {
                UserId = UserId,
                Name = Name,
                Header = Header,
                Footer = Footer,
                CurrencyId = CurrencyId,
                Items = Items.Select(item => new BudgetItem
                {
                    Name = item.Name,
                    Position = item.Position,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Duration = item.Duration,
                    DurationTimeUnit = item.DurationTimeUnit
                }).ToList(),
                IsTemplate = isTemplate,
                CreationDate = creationDate ?? DateTime.UtcNow
            };
        }
    }
}
