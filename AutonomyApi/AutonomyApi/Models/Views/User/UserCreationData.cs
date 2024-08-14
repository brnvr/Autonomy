using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.Views.User
{
    public class UserCreationData : UserUpdateData
    {
        [MinLength(Constants.Password.MinLength)]
        [MaxLength(Constants.Password.MaxLength)]
        public required string Password { get; set; }
    }
}
