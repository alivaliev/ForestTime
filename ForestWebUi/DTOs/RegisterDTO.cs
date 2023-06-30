using System.ComponentModel.DataAnnotations;

namespace ForestWebUi.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Ad 15 simvoldan artiq olmamalidir")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}

