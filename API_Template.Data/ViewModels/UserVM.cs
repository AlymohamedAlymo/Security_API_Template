using System.ComponentModel.DataAnnotations;

namespace API_Template.Data.ViewModels
{
    public class UserVM
    {
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public required string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public required string Password { get; set; } = string.Empty;

    }
}
