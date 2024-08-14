namespace AutonomyApi.Models.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required DateTime RegistrationDate { get; set; }
    }
}