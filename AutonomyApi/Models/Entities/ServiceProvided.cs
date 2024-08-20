namespace AutonomyApi.Models.Entities
{
    public class ServiceProvided
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public required string ServiceName { get; set; }
        public required List<ServiceProvidedClient> Clients { get; set; }
        public required DateTime Date { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
