using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.Views.User
{
    public class UserUpdateData
    {
        [MinLength(Constants.Name.MinLength)]
        [MaxLength(Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}
