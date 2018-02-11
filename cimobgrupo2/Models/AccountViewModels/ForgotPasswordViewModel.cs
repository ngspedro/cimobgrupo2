using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    /// <summary>Viewmodel para página de recuperação de password</summary>
    /// <remarks>Possui apenas uma propriedade para introdução do email a receber o link de recuperação</remarks>
    public class ForgotPasswordViewModel
    {
        /// <summary>Propriedade correspondente email da conta a recuperar</summary>
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}
