using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    /// <summary>Viewmodel para o segundo passo da recuperação de password</summary>
    /// <remarks>Possui as propriedades necessárias para tal (nova password, confirmação de password, etc.)</remarks>
    public class ResetPasswordViewModel
    {
        /// <summary>Propriedade correspondente à password que se pretende</summary>
        [Required(ErrorMessage = "Nova Password obrigatória.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password:")]
        public string Password { get; set; }

        /// <summary>Propriedade correspondente à confirmação da nova password</summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Password:")]
        [Compare("Password", ErrorMessage = "As passwords não coincidem!")]
        public string ConfirmPassword { get; set; }

        /// <summary>Propriedade correspondente ao código necessário para recuperação de password</summary>
        public string Code { get; set; }

        /// <summary>Propriedade correspondente ao id do utilizador a recuperar a password</summary>
        public string UserId { get; set; }
    }
}
