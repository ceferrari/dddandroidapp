using System;

namespace App.Application.ViewModels
{
    public class CustomerViewModel
    {
        //[Key]
        //[DisplayName("Código")]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "O Nome é obrigatório")]
        //[StringLength(100, MinimumLength = 6, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
        //[DisplayName("Nome")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "O E-mail é obrigatório")]
        //[EmailAddress]
        //[DisplayName("E-mail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        //[DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }
    }
}
