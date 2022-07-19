using System.ComponentModel.DataAnnotations;

namespace IdentityServerManagement.ClientOne.Models
{
    public class LogInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
