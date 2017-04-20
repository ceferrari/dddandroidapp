using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Senhas não conferem")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        public string ConfirmPassword { get; set; }
    }
}
