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
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contacto obrigatório.")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Username obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password obrigatória.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
