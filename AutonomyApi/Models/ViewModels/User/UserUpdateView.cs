using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.User
{
    public class UserUpdateView
    {
        [MinLength(Constants.Name.MinLength)]
        [MaxLength(Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}
