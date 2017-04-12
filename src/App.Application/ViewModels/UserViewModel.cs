using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Application.ViewModels
{
    public class UserViewModel
    {
        [Key]
        [DisplayName("Código")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }
    }
}
