namespace AutonomyApi.Models.Entities
{
    public class Schedule
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required List<Client> Clients { get; set; }
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        public required DateTime Date { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
