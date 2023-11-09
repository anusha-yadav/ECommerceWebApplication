using System.ComponentModel.DataAnnotations;

namespace E_Commerce_WebApplication.Models
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
