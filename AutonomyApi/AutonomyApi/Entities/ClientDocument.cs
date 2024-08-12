using AutonomyApi.Enums;
using AutonomyApi.ValueTypes.Documents;

namespace AutonomyApi.Entities
{
    public class ClientDocument
    {
        public DocumentType Type { get; set; }
        public required string Value { get; set; }
        public int ClientId { get; private set; }

        public Document GetDocument()
        {
            if (Type == DocumentType.Cpf)
            {
                return new Cpf(Value);
            }

            throw new NotImplementedException($"{nameof(DocumentType)}.{Type} not mapped to a {nameof(Document)}.");
        }
    }
}