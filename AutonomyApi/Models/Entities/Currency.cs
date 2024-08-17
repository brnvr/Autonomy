namespace AutonomyApi.Models.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
    }
}
