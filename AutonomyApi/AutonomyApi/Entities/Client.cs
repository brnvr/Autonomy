namespace AutonomyApi.Entities
{
    public class Client
    {
        public int Id { get; private set; }
        internal int UserId { get; set; }
        public required string Name { get; set; }
        public required List<ClientDocument> Documents { get; set; }
        public DateTime RegistrationDate { get; private set; } = DateTime.UtcNow;
    }
}