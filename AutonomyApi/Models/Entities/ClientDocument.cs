using AutonomyApi.Enums;

namespace AutonomyApi.Models.Entities
{
    public class ClientDocument
    {
        public int ClientId { internal get; set; }
        public required DocumentType Type { get; set; }
        public required string Value { get; set; }
    }
}