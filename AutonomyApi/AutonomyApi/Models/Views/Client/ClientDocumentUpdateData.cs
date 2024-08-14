using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.Views.Client
{
    public class ClientDocumentUpdateData
    {
        [MinLength(Constants.Document.MinLength)]
        [MaxLength(Constants.Document.MaxLength)]
        public required string Value { get; set; }
    }
}