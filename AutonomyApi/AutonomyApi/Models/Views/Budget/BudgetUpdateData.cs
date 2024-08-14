using AutonomyApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using AutonomyApi.WebService.Validations;

namespace AutonomyApi.Models.Views.Budget
{
    public class BudgetUpdateData
    {
        [MinLength(Constants.Name.MinLength)]
        [MaxLength(Constants.Name.MaxLength)]
        public required string Name { get; set; }

        [MinLength(Constants.Description.MinLength)]
        [MaxLength(Constants.Description.MaxLength)]
        public string? Header { get; set; }

        [MinLength(Constants.Description.MinLength)]
        [MaxLength(Constants.Description.MaxLength)]
        public string? Footer { get; set; }

        [MinCount(1)]
        [MaxCount(50)]
        public required List<BudgetItem> Items { get; set; }
    }
}