using AutonomyApi.Enums;
using AutonomyApi.ValidationAttributes;

namespace AutonomyApi.Models.ViewModels.Client
{
    public class ClientDocumentUpdateView
    {
        public required DocumentType Type { get; set; }
        [Document(nameof(Type))]
        public required string Value { get; set; }
    }
}