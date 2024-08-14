namespace AutonomyApi.Models.Entities
{
    public class Client
    {
        public int Id { get; private set; }
        public required int UserId { internal get; set; }
        public required string Name { get; set; }
        public required List<ClientDocument> Documents { get; set; }
        public required DateTime RegistrationDate { get; set; }
    }
}