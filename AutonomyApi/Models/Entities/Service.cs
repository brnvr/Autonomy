namespace AutonomyApi.Models.Entities
{
    public class Service
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? BudgetTemplateId { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
