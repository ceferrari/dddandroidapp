using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Lembrar-me?")]
        public bool RememberMe { get; set; }
    }
}
