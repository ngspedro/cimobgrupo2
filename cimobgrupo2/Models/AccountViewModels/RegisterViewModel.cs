using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    /// <summary>Viewmodel para registo</summary>
    /// <remarks>Possui as propriedades necessárias para tal (nome, contacto, etc.)</remarks>
    public class RegisterViewModel
    {
        /// <summary>Propriedade correspondente ao nome da conta a criar</summary>
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Nome { get; set; }

        /// <summary>Propriedade correspondente à data de nascimento</summary>
        [Required(ErrorMessage = "Data de Nascimento obrigatória.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "Data de Nascimento inválida.")]
        [Maiores17(ErrorMessage = "Só aceitamos registos a maiores de 17 anos.")]
        public string DataNascimento { get; set; }

        /// <summary>Propriedade correspondente ao email da conta a criar</summary>
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        /// <summary>Propriedade correspondente ao contacto</summary>
        [Required(ErrorMessage = "Contacto obrigatório.")]
        [RegularExpression(@"^9[1236][0-9]{7}$|^2[3-9][1-9][0-9]{6}$|^2[12][0-9]{7}$", ErrorMessage = "Contacto inválido.")]
        public string Contato { get; set; }

        /// <summary>Propriedade correspondente ao username da conta</summary>
        [Required(ErrorMessage = "Username obrigatório.")]
        public string Username { get; set; }

        /// <summary>Propriedade correspondente à password da conta</summary>
        [Required(ErrorMessage = "Password obrigatória.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>Propriedade correspondente à confirmação de password</summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As passwords não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
