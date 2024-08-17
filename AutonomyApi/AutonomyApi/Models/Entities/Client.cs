using Microsoft.AspNetCore.Mvc;

namespace AutonomyApi.Models.Entities
{
    public class Client
    {
        public required int UserId { internal get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<ClientDocument> Documents { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}