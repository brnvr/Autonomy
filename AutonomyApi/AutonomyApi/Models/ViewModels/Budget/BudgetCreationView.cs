using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetCreationView  
    {
        [Length(Constants.Name.MinLength, Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}
