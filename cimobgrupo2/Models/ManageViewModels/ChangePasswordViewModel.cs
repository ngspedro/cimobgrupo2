using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    /// <summary>Viewmodel para alteração de password de conta</summary>
    /// <remarks>Possui as propriedades necessárias para tal (password antiga, nova password, etc.)</remarks>
    public class ChangePasswordViewModel
    {
        /// <summary>Propriedade correspondente à password atual</summary>
        [Required(ErrorMessage = "Password Atual obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Atual:")]
        public string OldPassword { get; set; }

        /// <summary>Propriedade correspondente à nova password pretendida</summary>
        [Required(ErrorMessage = "Nova Password obrigatória")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password:")]
        public string NewPassword { get; set; }

        /// <summary>Propriedade correspondente à confirmação da password pretendida</summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Password:")]
        [Compare("NewPassword", ErrorMessage = "As passwords não coincidem.")]
        public string ConfirmPassword { get; set; }

        /// <summary>Propriedade correspondente à mensagem do estado da mudança de password</summary>
        public string StatusMessage { get; set; }
    }
}
