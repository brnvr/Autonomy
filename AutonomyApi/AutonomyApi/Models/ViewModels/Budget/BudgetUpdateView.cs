using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetUpdateView
    {
        [Length(Constants.Name.MinLength, Constants.Name.MaxLength)]
        public required string Name { get; set; }

        [Length(Constants.Description.MinLength, Constants.Description.MaxLength)]
        public string? Header { get; set; }

        [Length(Constants.Description.MinLength, Constants.Description.MaxLength)]
        public string? Footer { get; set; }
    }
}