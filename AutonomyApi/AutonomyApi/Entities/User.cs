namespace AutonomyApi.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public DateTime RegistrationDate { get; } = DateTime.UtcNow;
    }
}