using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.User
{
    public class UserCreationView : UserUpdateView
    {
        [MinLength(Constants.Password.MinLength)]
        [MaxLength(Constants.Password.MaxLength)]
        public required string Password { get; set; }
    }
}
