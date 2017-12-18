using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento obrigatória.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "Data de Nascimento inválida.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contacto obrigatório.")]
        [RegularExpression(@"^9[1236][0-9]{7}$|^2[3-9][1-9][0-9]{6}$|^2[12][0-9]{7}$", ErrorMessage = "Contacto inválido.")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Username obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password obrigatória.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As passwords não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
